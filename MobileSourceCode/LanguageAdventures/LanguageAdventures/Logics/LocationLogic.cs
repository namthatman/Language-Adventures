using System;
using LanguageAdventures.Models;
using LanguageAdventures.URLs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LanguageAdventures.Logics
{
    // handles logic for sending http request to web server via api uri
    // requests for Location stuff
    class LocationLogic
    {
        // expects web server to receive the location of the device
        public async static void PutLocation(String location) // TODO: improve by using try-catch to handle exceptions
        {
            var uri = new Uri(URL.ZONE + URL.LOCATION);
            try
            {
                /*using (HttpClient client = new HttpClient())
                {
                    string teamId = (string) Application.Current.Properties["teamID"];
                    var response = await client.PutAsync(uri + "/"+teamId+"/" + location, new StringContent(""));
                }*/
                string teamId = (string)Application.Current.Properties["teamID"];
                var response = await App.client.PutAsync(uri + "/" + teamId + "/" + location, new StringContent(""));
                //perhaps we should return if the location message was able to send through?
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
        }
    }
}
