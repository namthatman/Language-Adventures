using LanguageAdventures.Logics;
using LanguageAdventures.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Review page display a brief introduction of the selected adventure,
// User clicks Start button at the end to begin this adventure.
namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewPage : ContentPage
    {

        public Adventure adventure { get; private set; }
        private Session session { get; set; }

        public ReviewPage(Adventure e)
        {
            InitializeComponent();
            adventure = e;
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing(); 
            await App.isNetworkAccess();

            session = SessionLogic.GetSession(App.myTeam.teamID, adventure.adventureID).Result;
            if (session == null) //this is the first time the team is playing this adventure
            {
                Continue.IsVisible = false;
                Grid.SetColumnSpan(Start, 2);
            }
            else
            {
                Continue.IsVisible = true;
            }

        }

        // To initiate the adventure
        private async void Start_Clicked(object sender, EventArgs e)
        {
            Start.IsEnabled = false; // disable the button to avoid multiple clicks.

            actIndicator.IsRunning = true;
            await App.isNetworkAccess();

            if (adventure.waypointID == null) // handle error when no starting waypoint found
            {
                actIndicator.IsRunning = false;
                await DisplayAlert("Error", "Could not find the stating waypoint! This adventure is current unavailable or under development.", "Cancel");
                Start.IsEnabled = true; // enable the button.
                return;
            }

            session = await SessionLogic.GetSession(App.myTeam.teamID, adventure.adventureID);
            if (session == null) //this is the first time the team is playing this adventure
            {
                await SessionLogic.PostSession(adventure); //create a new session for this adventure and team
                session = await SessionLogic.GetSession(App.myTeam.teamID, adventure.adventureID);
            }
            else
            {
                actIndicator.IsRunning = false;
                bool sng = await DisplayAlert("Warning", "Starting new game will cause your past save of this adventure be removed!", "Start new game", "Cancel");
                if (sng == true)
                {
                    actIndicator.IsRunning = true;
                    //Reset game state
                    await SessionLogic.PutSession(App.myTeam.teamID, adventure.adventureID, adventure.waypointID.Value);
                    session = await SessionLogic.GetSession(App.myTeam.teamID, adventure.adventureID);
                }
                else
                {
                    Start.IsEnabled = true; // enable the button.
                    return;
                }
            }

            Waypoint wp = await WaypointLogic.GetWaypoint(session.WaypointID);
            App.atWaypointID = session.WaypointID;
            App.atAdventureID = session.AdventureID;

            App.reachableWaypoints = await AdventureMapLogic.GetAdventureMap(App.atWaypointID);
            App.getLocation();
            actIndicator.IsRunning = false;
            Start.IsEnabled = true; // enable the button.
            await Navigation.PushAsync(new StoryPage(adventure.title, wp));
        }

        private async void Continue_Clicked(object sender, EventArgs e)
        {
            Continue.IsEnabled = false; // disable the button to avoid multiple clicks.

            actIndicator.IsRunning = true;
            await App.isNetworkAccess();

            //session = SessionLogic.GetSession(App.myTeam.teamID, adventure.adventureID).Result;

            Waypoint wp = await WaypointLogic.GetWaypoint(session.WaypointID);
            App.atWaypointID = session.WaypointID;
            App.atAdventureID = session.AdventureID;

            App.reachableWaypoints = await AdventureMapLogic.GetAdventureMap(App.atWaypointID);
            App.getLocation();
            actIndicator.IsRunning = false;
            Continue.IsEnabled = true; // enable the button.
            await Navigation.PushAsync(new StoryPage(adventure.title, wp));
        }
    }
}