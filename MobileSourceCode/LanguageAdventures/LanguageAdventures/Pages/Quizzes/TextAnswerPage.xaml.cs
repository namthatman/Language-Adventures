using LanguageAdventures.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Quiz that would accept text as an input
namespace LanguageAdventures.Pages.Quizzes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextAnswerPage : ContentPage
    {
        public Challenge Challenge { get; private set; }

        public TextAnswerPage() // an example Text quiz
        {
            InitializeComponent();

            Challenge = new Challenge
            {
                ChallengeID = 2,
                ChallengeType = "Text",
                ChallengeDetail = "What is the name of this app's dev team ?",
                CorrectAnswer = "moths"
            };

            BindingContext = this;
        }

        public TextAnswerPage(Challenge challenge, bool isCompleted)
        {
            InitializeComponent();
            Challenge = challenge;
            BindingContext = this;
        }

        // handles Submit button clicked to check correctness
        private void Submit_Clicked(object sender, EventArgs e)
        {
            bool isAnswerEmpty = string.IsNullOrEmpty(MyAnswer.Text);
            if (!isAnswerEmpty)
            {
                bool isAnswerCorrect = MyAnswer.Text.Equals(Challenge.CorrectAnswer, StringComparison.InvariantCultureIgnoreCase);
                if (isAnswerCorrect)
                {
                    DisplayAlert("Congratulations!", "Your answer is correct. Go to the next story point!", "Ok");
                    StoryPage.isCompleted = true;
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Oops!", "Your answer is incorrect", "Ok");
                }
            }
            else
            {
                DisplayAlert("Oops!", "Your answer is incorrect", "Ok");
            }
        }
    }
}