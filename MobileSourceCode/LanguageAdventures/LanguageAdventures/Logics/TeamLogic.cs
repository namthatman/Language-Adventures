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
    // requests for Team stuff
    class TeamLogic
    {
        // expects web server to return the corresponding team information of the student
        public async static Task<Team> GetTeam(string studentID) // TODO: improve by using try-catch to handle exceptions
        {
            Team team = new Team();
            var uri = new Uri(URL.ZONE + URL.STUDENT);
            /*using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri + "/" + studentID);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    team = JsonConvert.DeserializeObject<Team>(json);
                }                  
            }*/
            try
            {
                var response = await App.client.GetAsync(uri + "/" + studentID);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    team = JsonConvert.DeserializeObject<Team>(json);
                }
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
            return team;
        }
    }
}
