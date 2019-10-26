using LanguageAdventures.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Quiz that would is in a multiple-choice format
namespace LanguageAdventures.Pages.Quizzes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MCQPage : ContentPage
    {
        public Challenge Challenge { get; private set; }
        public IList<string> Choices { get; private set; }
        public string MyAnswer { get; private set; }

        public MCQPage() // an example MCQ
        {
            InitializeComponent();

            Challenge = new Challenge
            {
                ChallengeID = 1,
                ChallengeType = "MCQ",
                ChallengeDetail = "Choose the largest.",
                CorrectAnswer = "largest"
            };
            Choices = new List<string>();
            Choices.Add("113");
            Choices.Add("69");
            Choices.Add("7");
            Choices.Add("largest");
            MyAnswer = "";

            BindingContext = this;
        }

        public MCQPage(Challenge challenge, bool isCompleted)
        {
            InitializeComponent();

            Challenge = challenge;
            Choices = new List<string>();

            // separate question text and choices from ChallengeDetail
            char[] chars = Challenge.ChallengeDetail.ToCharArray();
            string question = "";
            int idx = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '$' && idx == 0)
                {
                    question = Challenge.ChallengeDetail.Substring(idx, i);
                    idx = i + 1;
                }
                else if (chars[i] == '$' && idx != 0)
                {
                    Choices.Add(Challenge.ChallengeDetail.Substring(idx, i - idx));
                    idx = i + 1;
                }
            }
            Challenge.ChallengeDetail = question;

            MyAnswer = "";
            BindingContext = this;
        }

        // handles Submit button clicked to check correctness
        private void Submit_Clicked(object sender, EventArgs e)
        {
            bool isAnswerCorrect = MyAnswer.Equals(Challenge.CorrectAnswer, StringComparison.InvariantCultureIgnoreCase);
            if (isAnswerCorrect)
            {
                DisplayAlert("Congratulations!", "Your answer is correct. Go to the next story point!", "Ok");
                StoryPage.isCompleted = true;
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Oops!", "Your answer is incorrect. Try again!", "Ok");
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MyAnswer = e.Item as string;
        }
    }
}