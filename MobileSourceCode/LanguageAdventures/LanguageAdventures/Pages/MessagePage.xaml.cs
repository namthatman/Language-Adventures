using LanguageAdventures.Logics;
using LanguageAdventures.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Message page displays the messages sent from the game master.
namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        //public string welcomeMessage { get; set; }
        public IList<Message> Messages { get; private set; }
        public MessagePage()
        {
            InitializeComponent();
            gmAvatar.Source = ImageSource.FromResource("LanguageAdventures.moths_logo.png", typeof(MessagePage)); // avatar of game master
            refreshButton.Source = ImageSource.FromResource("LanguageAdventures.refresh_button.png", typeof(MessagePage)); // refresh button
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.isNetworkAccess();

            Messages = await MessageLogic.GetMessages(App.myTeam.teamID);
            if (Messages.Any())
            {
                messageListView.ItemsSource = Messages;
            }
        }

        private void RefreshButton_Clicked(object sender, System.EventArgs e)
        {
            OnAppearing();
        }
    }
}