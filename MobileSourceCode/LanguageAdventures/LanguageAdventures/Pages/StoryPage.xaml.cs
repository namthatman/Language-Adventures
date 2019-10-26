using LanguageAdventures.Logics;
using LanguageAdventures.Models;
using LanguageAdventures.Pages.Quizzes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Story page displays the information of a story node/waypoint
namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoryPage : ContentPage
    {
        public string adventureTitle { get; private set; }
        public Waypoint waypoint { get; private set; }
        public Button challengeBtn { get; private set; }

        public static bool isCompleted = true;
        public bool isEndpoint = false;

        public StoryPage(string adventureTitle, Waypoint waypoint)
        {
            InitializeComponent();

            //disables navigation back button
            NavigationPage.SetHasBackButton(this, false);

            this.adventureTitle = adventureTitle;

            this.waypoint = waypoint;

            this.challengeBtn = this.FindByName<Button>("challenge_btn");

            homeButton.IconImageSource = ImageSource.FromResource("LanguageAdventures.home_icon.png", typeof(StoryPage)); // refresh button

            if (App.reachableWaypoints.Count == 0)//this is the last waypoint in this story line
            {
                continue_btn.Text = "Complete";
                isEndpoint = true;
            }


            // Determine whether the waypoint has a challenge or not
            // In this case, if challengId is 1, then there are no challenges
            if (waypoint.ChallengeId == null)
            {
                challengeBtn.IsVisible = false;
                isCompleted = true;
            }
            else
            {
                challengeBtn.IsVisible = true;
                challenge_btn.IsEnabled = true;
                isCompleted = false;
            }

            BindingContext = this;
        }

        // To choose what type of challenge is going to be shown by the Challenge Page
        async void GotoChallenge(object sender, EventArgs e)
        {
            challenge_btn.IsEnabled = false; // disable the button to avoid multiple clicks.
            await App.isNetworkAccess();

            Challenge challenge = await ChallengeLogic.GetChallenge(waypoint.ChallengeId.Value);

            if (challenge.ChallengeType == "MCQ")
            {
                await Navigation.PushAsync(new MCQPage(challenge, isCompleted)); // isCompleted can be used to autofill textbox if previously done
            }
            else if (challenge.ChallengeType == "Text")
            {
                await Navigation.PushAsync(new TextAnswerPage(challenge, isCompleted)); // isCompleted can be used to autofill textbox if previously done
            }
            else if (challenge.ChallengeType == "Photo")
            {
                await Navigation.PushAsync(new PhotoPage());
            }
            else if (challenge.ChallengeType == "Audio")
            {
                await Navigation.PushAsync(new AudioPage());
            }
        }

        // To support movement between waypoint, it will check whether the players are allowed to move to the next waypoint or not
        async void OnWaypointAlertClick(object sender, EventArgs e)
        {
            continue_btn.IsEnabled = false; // disable the button to avoid multiple clicks.

            actIndicator.IsRunning = true;
            await App.isNetworkAccess();
          
            if (isCompleted == false) // requests to finished challenge before move to the next waypoint
            {
                actIndicator.IsRunning = false;
                await DisplayAlert("Failed", "You have not completed the challenge yet. Try again!", "Ok");
                continue_btn.IsEnabled = true; // enable the button.
                return;
            }

            if (isEndpoint == true) // handles the last waypoint completed.
            {
                actIndicator.IsRunning = false;
                await DisplayAlert("Congratulation", "You have completed '" + adventureTitle + "'!", "Ok");
                continue_btn.Text = "Completed";
                continue_btn.IsEnabled = false;
                return;
            }

            await App.getLocation(); // the main method to compare location

            Waypoint wp = await WaypointLogic.GetWaypoint(App.atWaypointID);
            bool reached = false;

            foreach (Waypoint w in App.reachableWaypoints)
            {
                if (w.WaypointId == wp.WaypointId)
                {
                    reached = true;
                }
            }

            
            if (reached == false) // alerts if not reached a new waypoint yet
            {
                actIndicator.IsRunning = false;
                await DisplayAlert("Failed", "You have not reached a new story point yet. Keep searching!", "Ok");
                continue_btn.IsEnabled = true; // enable the button.
                return;
            }

            //Save new state
            await SessionLogic.PutSession(App.myTeam.teamID, App.atAdventureID, App.atWaypointID);

            // opens new Story page with the content of the next waypoint
            App.reachableWaypoints = await AdventureMapLogic.GetAdventureMap(App.atWaypointID);
            actIndicator.IsRunning = false;
            continue_btn.IsEnabled = true; // enable the button.

            await Navigation.PushAsync(new StoryPage(adventureTitle, wp));
        }

        // Button to move back to the homepage
        async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        //disables hardware backbutton on android phones
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        // Visualization to indicate whether the challenge is completed or not
        protected override void OnAppearing()
        {
            if (isCompleted == false)
            {
                challengeBtn.TextColor = Color.Red;
            }
            else
            {
                challengeBtn.TextColor = Color.LightGreen;
            }
            challenge_btn.IsEnabled = true;
        }
    }
}