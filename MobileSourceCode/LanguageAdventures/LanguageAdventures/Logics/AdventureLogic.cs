using LanguageAdventures.Models;
using LanguageAdventures.URLs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LanguageAdventures.Logics
{
    // handles logic for sending http request to web server via api uri
    // requests for Adventures stuff
    class AdventureLogic
    {
        // expects web server to return list of all available adventures
        public static async Task<List<Adventure>> GetAdventures() // @TODO: improve by using try-catch to handle exceptions
        {
            List<Adventure> adventures = new List<Adventure>();
            try
            {
                var uri = new Uri(URL.ZONE + URL.ADVENTURE);
                /*using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    var json = await response.Content.ReadAsStringAsync();
                    adventures = JsonConvert.DeserializeObject<List<Adventure>>(json);
                }*/
                var response = await App.client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                adventures = JsonConvert.DeserializeObject<List<Adventure>>(json);
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
            
            return adventures;
        }
    }
}
