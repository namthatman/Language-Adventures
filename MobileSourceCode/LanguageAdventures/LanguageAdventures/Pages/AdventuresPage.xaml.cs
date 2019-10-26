using LanguageAdventures.Logics;
using LanguageAdventures.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Adventures page displays the list of all available adventures,
// User selected one adventure to view the adventure description.
namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdventuresPage : ContentPage
    {
        public IList<Adventure> Adventures { get; private set; }

        public AdventuresPage()
        {
            InitializeComponent();
            Adventures = new List<Adventure>();
            refreshButton.Source = ImageSource.FromResource("LanguageAdventures.refresh_button.png", typeof(AdventuresPage)); // refresh button
        }

        // refresh adventures list every time the page on appearring
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            adventureListView.IsEnabled = true;
            await App.isNetworkAccess();
            Adventures = await AdventureLogic.GetAdventures();
            if (Adventures.Any())
            {
                adventureListView.ItemsSource = Adventures;
            }          
        }

        private void AdventureListView_ItemSelected(Adventure sender, SelectedItemChangedEventArgs e)
        {

        }

        // Move to adventure's review page to check it's description
        private async void AdventureListView_ItemTapped(Adventure sender, ItemTappedEventArgs e)
        {
            adventureListView.IsEnabled = false;
            var selectedAdventure = e.Item as Adventure;
            await Navigation.PushAsync(new ReviewPage(selectedAdventure));
        }

        private void RefreshButton_Clicked(object sender, System.EventArgs e)
        {
            OnAppearing();
        }
    }
}