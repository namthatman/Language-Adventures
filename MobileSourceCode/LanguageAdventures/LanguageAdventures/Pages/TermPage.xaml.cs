using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LanguageAdventures.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        public string gpsPolicy { get; set; }
        public TermPage()
        {
            InitializeComponent();
            gpsPolicy = "Language Adventures requires the permission to access the data related to the position, the location, coordinates of Global Position System (GPS) " +
                "of the users who use this app on mobile devices, including mobile phones, smartphones, tablets to collect resources for business use do so in a safe, secure manner. " +
                "The GPS data collected allows the app to provide all functional gameplay aspects to the user. Also, it is designed to maximize the degree to which private and " +
                "confidential data is protected from both deliberate and inadvertent exposure and/or breach to another third party.\n\n" +
                "In addition, this app collects the mention data only after being granted access permission by the users, and only when the app is active. It is our business duty to " +
                "guarantee that no and absolutely no data collection occurs in the background of mobile devices when the app is inactive as well as after the users stop using the app. " +
                "The users have the full right of revocation the granted permission to this app at any time.";
            BindingContext = this;
        }

        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void AgreeBox_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {

        }
    }
}