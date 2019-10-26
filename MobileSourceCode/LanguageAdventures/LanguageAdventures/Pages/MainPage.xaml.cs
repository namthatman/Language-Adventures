using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageAdventures.Logics;
using LanguageAdventures.Pages;
using LanguageAdventures.Themes;
using Xamarin.Forms;

namespace LanguageAdventures.Pages
{
    // Main page of the application is the login page.
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // declares the dictionary for themes
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries; 
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new LightTheme()); // add Light theme by default

            logoImage.Source = ImageSource.FromResource("LanguageAdventures.logo.png", typeof(MainPage)); // logo of the app on the top of login page
        }

        private void Darkmode_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // handles switching theme, the current theme is remove then the new theme is added into the dictionary
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            mergedDictionaries.Clear();
            if (e.Value)
            {
                mergedDictionaries.Add(new DarkTheme());
            }
            else
            {
                mergedDictionaries.Add(new LightTheme());
            }
        }

        private async void JoinButton_Clicked(object sender, EventArgs e)
        {
            // handles button clicked in login page
            joinButton.IsEnabled = false; // disable the button to avoid multiple clicks.


            // checks the input entry is not empty
            bool isTeamEmpty = string.IsNullOrEmpty(teamEntry.Text);
            if (isTeamEmpty)
            {
                await DisplayAlert("Failure", "Invalid student ID", "Ok");
            }
            else
            {
                // sends http request via TeamLogic class for team authentication
                App.myTeam = await TeamLogic.GetTeam(teamEntry.Text);
                if (App.myTeam.name != null) //student id query returned a team
                {
                    Application.Current.Properties["teamID"] = "" + App.myTeam.teamID;
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Failure", "Student ID not in any teams", "Ok");
                }
            }
            joinButton.IsEnabled = true; // enable the button.
        }
    }
}
