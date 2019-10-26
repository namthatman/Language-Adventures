using LanguageAdventures.Models;
using Plugin.AudioRecorder;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Quiz that would accept audio as an input
namespace LanguageAdventures.Pages.Quizzes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AudioPage : ContentPage
    {
        public Challenge Challenge { get; private set; }
        public string MyAnswer { get; set; }

        public AudioRecorderService recorder;
        public AudioPlayer player;
        public AudioPage() // an example Audio quiz
        {
            InitializeComponent();

            Challenge = new Challenge
            {
                ChallengeID = 3,
                ChallengeType = "Audio",
                ChallengeDetail = "Pronounce the word 'Antidisestablishmentarianism'.",
                CorrectAnswer = "Antidisestablishmentarianism"
            };
            MyAnswer = "";

            recorder = new AudioRecorderService
            {
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                StopRecordingOnSilence = true
            };

            player = new AudioPlayer();

            Record.IsEnabled = true; // Initiate button.
            Stop.IsEnabled = false; // Initiate button.
            Play.IsVisible = false; // Initiate button.
            Play.IsEnabled = false;

            BindingContext = this;
        }

        // TODO: handles Record button clicked to open device recorder
        async void Record_Clicked(object sender, EventArgs e)
        {
            Record.IsEnabled = false; // avoid multiple clicks on the button.
            Play.IsEnabled = false; // disable Play while recording.
            if (!recorder.IsRecording)
            {
                Stop.IsEnabled = true; // enable Stop button after start recording.
                Record_Status.Text = "[Status: RECORDING]";
                Record_Status.TextColor = Color.LightGreen;
                var audioRecordTask = await recorder.StartRecording();

                await audioRecordTask;
            }
            Record_Status.Text = "[Status: STOP]";
            Record_Status.TextColor = Color.Red;
            Stop.IsEnabled = false; // disable Stop after recording.
            Record.IsEnabled = true; // enable the button.
            Play.IsVisible = true; // enable Play button after finish recording.
            Play.IsEnabled = true; // enable the button.
        }

        async void Record_Stopped(object sender, EventArgs e)
        {
            Stop.IsEnabled = false; // avoid multiple clicks on the button.
            if (recorder.IsRecording)
            {
                Record_Status.Text = "[Status: STOP]";
                Record_Status.TextColor = Color.Red;
                await recorder.StopRecording();
            }
            Play.IsVisible = true; // enable Play button after finish recording.
            Play.IsEnabled = true; // enable the button.
        }

        void Record_Played(object sender, EventArgs e)
        {
            try
            {
                Play.IsEnabled = false; // avoid multiple clicks on the button.
                var filePath = recorder.GetAudioFilePath();

                if (filePath != null)
                {
                    Record_Status.Text = "[Status: PLAYING]";
                    Record_Status.TextColor = Color.Aqua;
                    player.Play(filePath);
                }
                Play.IsEnabled = true; // enable the button.
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Submitted!", "Your answer is submitted,\nPlease wait for verification.", "Ok");
        }
    }
}