using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LanguageAdventures.Pages;
using Xamarin.Essentials;
using LanguageAdventures.Logics;
using LanguageAdventures.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace LanguageAdventures
{
    // this main class of the project
    public partial class App : Application
    {
        public static HttpClient client = new HttpClient();
        public static List<Waypoint> reachableWaypoints { get; set; } //set of reachable waypoints (ones that should change mobile content when reached)
        public static int atWaypointID { get; set; }//the waypoint the current phone is at
        public static int atAdventureID { get; set; }//the current adventure being played
        public static Team myTeam { get; set; } //the team the current user is at

        public App()
        {
            InitializeComponent();
            
            reachableWaypoints =  new List<Waypoint>();

            myTeam = new Team();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            getLocation();
            isNetworkAccess(); // check network connectivity at start
            //run get location at specified time intervals while the app is open
            Device.StartTimer(TimeSpan.FromSeconds(15), () => {
                Task.Factory.StartNew(async () =>
                    {
                        await getLocation();
                    });
                return true;
                });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            getLocation();
            isNetworkAccess();
        }

        //function to get location, 
        // is called: When app is opened (from sleeping or at first open) and repeatadly at specified times in the application
        public async static Task<bool> getLocation()
        {
            var rtrn = new TaskCompletionSource<bool>();

            //If the student has not logged into a team yet, dont continue
            if (Application.Current.Properties.ContainsKey("teamID") == false)
            {
                rtrn.SetResult(false);
                return rtrn.Task.Result;
            }
            
            try
            {
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);
                

                if (location != null)
                {
                    await App.isNetworkAccess(); // check network connectivity

                    //Send location to web api
                    LocationLogic.PutLocation(location.Longitude.ToString() + "|" + location.Latitude.ToString());
                        
                    //Check if the user has reached a reachable waypoint
                    foreach (Waypoint w  in reachableWaypoints)
                    {
                        //for every waypoint that is reachable (will advance the story), check if you are within 25 meters 
                        double dist = location.CalculateDistance(new Location(w.Latitude, w.Longitude), DistanceUnits.Kilometers);
                        if (dist < .025)
                        {
                            atWaypointID = w.WaypointId;
                        }
                    }
                }
                else
                {
                    //could not get location
                    rtrn.SetResult(false);
                    return rtrn.Task.Result;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await App.Current.MainPage.DisplayAlert("Warning!", "Turn on your gps locations", "Ok");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await App.Current.MainPage.DisplayAlert("Warning!", "Grant the permissions when the application starts", "Ok");
            }
            catch (Exception ex)
            {
                // Unable to get location
                //Console.WriteLine("MYERROR: " + ex.Message);
            }
           
            rtrn.SetResult(true);
            return rtrn.Task.Result;
        }

        // method to check network connectivity
        // if there is no network access, recheck the connectivity again
        public async static Task<bool> isNetworkAccess()
        {
            var rtrn = new TaskCompletionSource<bool>();
            var current = Connectivity.NetworkAccess;

            while (current != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection.", "Try again");
                current = Connectivity.NetworkAccess;
            }
            rtrn.SetResult(true);
            return rtrn.Task.Result;
        }
    }
}
