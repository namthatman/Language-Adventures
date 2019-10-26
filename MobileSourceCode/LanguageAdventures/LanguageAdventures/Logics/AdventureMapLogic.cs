using LanguageAdventures.Models;
using LanguageAdventures.URLs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAdventures.Logics
{
    // handles logic for sending http request to web server via api uri
    // requests for AdventureMap stuff
    class AdventureMapLogic
    {
        // expects web server to return list of all options for a single waypoint
        public async static Task<List<Waypoint>> GetAdventureMap(int waypointID) // TODO: improve by using try-catch to handle exceptions
        {

            List<AdventureMap> adventureMap = new List<AdventureMap>(); 
            var uri = new Uri(URL.ZONE + URL.ADVENTURE_MAP);
            List<Waypoint> reachableWaypoints = new List<Waypoint>();

            /*using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri + "/" + waypointID);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    adventureMap = JsonConvert.DeserializeObject<List<AdventureMap>>(json);

                    foreach (AdventureMap a in adventureMap)
                    {
                        Waypoint wp = await WaypointLogic.GetWaypoint(a.toWaypointID);
                        reachableWaypoints.Add(wp);
                    }                 
                }
            }*/
            try
            {
                var response = await App.client.GetAsync(uri + "/" + waypointID);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    adventureMap = JsonConvert.DeserializeObject<List<AdventureMap>>(json); 

                    foreach (AdventureMap a in adventureMap)
                    {
                        Waypoint wp = await WaypointLogic.GetWaypoint(a.toWaypointID);
                        reachableWaypoints.Add(wp);
                    }
                }
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
            return reachableWaypoints;
        }
    }
}
