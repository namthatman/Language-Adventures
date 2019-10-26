using LanguageAdventures.Pages.Quizzes;
using LanguageAdventures.Themes;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Setting page displays many setting option for the application.
namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public string myTeamName { get; set; }
        public SettingPage()
        {
            InitializeComponent();
            themePicker.Items.Add("Light");
            themePicker.Items.Add("Dark");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.isNetworkAccess();
            myTeamName = App.myTeam.name;
            MCQ.IsEnabled = true; // enable the button.
            TextAnswer.IsEnabled = true; // enable the button.
            Photo.IsEnabled = true; // enable the button.
            Audio.IsEnabled = true; // enable the button.
            policyButton.IsEnabled = true; // enable the button.
            BindingContext = this;
        }

        // To change the color theme of the app
        private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            string theme = (string)picker.SelectedItem;
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                switch (theme)
                {
                    case "Dark":
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case "Light":
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
            DisplayAlert("Theme", "Selected " + theme + " theme", "Ok");
        }

        // Below are methods to show an example on how the quizzes would look like
        private void MCQ_Clicked(object sender, EventArgs e)
        {
            MCQ.IsEnabled = false; // disable the button to avoid multiple clicks.
            Navigation.PushAsync(new MCQPage());
        }

        private void TextAnswer_Clicked(object sender, EventArgs e)
        {
            TextAnswer.IsEnabled = false; // disable the button to avoid multiple clicks.
            Navigation.PushAsync(new TextAnswerPage());
        }

        private void Photo_Clicked(object sender, EventArgs e)
        {
            Photo.IsEnabled = false; // disable the button to avoid multiple clicks.
            Navigation.PushAsync(new PhotoPage());
        }

        private void Audio_Clicked(object sender, EventArgs e)
        {
            Audio.IsEnabled = false; // disable the button to avoid multiple clicks.
            Navigation.PushAsync(new AudioPage());
        }

        private void PolicyButton_Clicked(object sender, EventArgs e)
        {
            policyButton.IsEnabled = false; // disable the button to avoid multiple clicks.
            Navigation.PushModalAsync(new TermPage());
        }
    }
}