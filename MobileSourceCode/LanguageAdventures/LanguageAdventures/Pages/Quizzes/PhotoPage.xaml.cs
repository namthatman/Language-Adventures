using LanguageAdventures.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Quiz that would accept image as an input
namespace LanguageAdventures.Pages.Quizzes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoPage : ContentPage
    {
        public Challenge Challenge { get; private set; }
        public string MyAnswer { get; set; }
        public PhotoPage() // an example Photo quiz
        {
            InitializeComponent();

            Challenge = new Challenge
            {
                ChallengeID = 3,
                ChallengeType = "Photo",
                ChallengeDetail = "Take a picture of an ibis",
                CorrectAnswer = "ibis"
            };
            MyAnswer = "";

            BindingContext = this;
        }

        public PhotoPage(Challenge challenge)
        {
            InitializeComponent();
            Challenge = challenge;
            BindingContext = this;
        }

        // TODO: handles Take Photo button clicked to open device camera
        private async void Take_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            {
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }

        // TODO: handles Upload Photo button clicked to open device gallery
        private async void Upload_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                PhotoImage.Source = ImageSource.FromStream(() => { return stream; });
            }

            //(sender as Button).IsEnabled = true;
            //DisplayAlert("Unavailable!", "This feature is under development.", "Ok");
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Submitted!", "Your answer is submitted,\nPlease wait for verification.", "Ok");
        }
    }
}