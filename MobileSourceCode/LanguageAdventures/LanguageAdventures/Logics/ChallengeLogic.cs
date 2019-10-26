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
    // requests for Challenge stuff
    class ChallengeLogic
    {
        // expects web server to return the corresponding challenge object
        public async static Task<Challenge> GetChallenge(int challengeID) // TODO: improve by using try-catch to handle exceptions
        {
            Challenge challenge = new Challenge();
            var uri = new Uri(URL.ZONE + URL.CHALLENGE);
            /*using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri + "/" + challengeID);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    challenge = JsonConvert.DeserializeObject<Challenge>(json);
                }
            }*/
            try
            {
                var response = await App.client.GetAsync(uri + "/" + challengeID);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    challenge = JsonConvert.DeserializeObject<Challenge>(json);
                }
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
            return challenge;
        }
    }
}
