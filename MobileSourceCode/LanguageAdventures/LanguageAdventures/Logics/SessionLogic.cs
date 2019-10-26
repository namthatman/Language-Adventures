using LanguageAdventures.Models;
using LanguageAdventures.URLs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LanguageAdventures.Logics
{
    // handles logic for sending http request to web server via api uri
    // requests for Session stuff
    class SessionLogic
    {
        // expects web server to receive and save the current session of the team
        public async static Task PostSession(Adventure adventure) // TODO: implement the method
        {

            var uri = new Uri(URL.ZONE + URL.SESSIONS);

            try
            {
                JObject jsonObject = new JObject();
                jsonObject.Add("teamID", App.myTeam.teamID);
                jsonObject.Add("adventureID", adventure.adventureID);
                jsonObject.Add("waypointID", adventure.waypointID);

                var response = await App.client.PostAsync(uri,
                    new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }

        }

        // expects web server to return the current session of the team
        public async static Task<Session> GetSession(int teamID, int adventureID) // TODO: implement the method
        {
            Session session = new Session();
            var uri = new Uri(URL.ZONE + URL.SESSIONS + "/" + teamID + "/" + adventureID);
            /*using (HttpClient client = new HttpClient())
            {
                var requestTask = client.GetAsync(uri);
                var response = Task.Run(() => requestTask);

                //var response = await client.GetAsync(uri);
                var json = await response.Result.Content.ReadAsStringAsync();
                session = JsonConvert.DeserializeObject<Session>(json);
            }*/
            try
            {
                var requestTask = App.client.GetAsync(uri);
                var response = Task.Run(() => requestTask);
                var json = await response.Result.Content.ReadAsStringAsync();
                session = JsonConvert.DeserializeObject<Session>(json);
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
            return session;
        }

        // expects web server to receive and update the current session of the team
        public async static Task PutSession(int teamID, int adventureID, int waypointID) // TODO: implement the method
        {
            var uri = new Uri(URL.ZONE + URL.SESSIONS);
            /*using (HttpClient client = new HttpClient())
            {
                //string teamId = (string)Application.Current.Properties["teamID"];   
                JObject jsonObject = new JObject();
                jsonObject.Add("waypointID", waypointID);

                var response = await client.PutAsync(uri + "/" + teamID + "/" + adventureID,
                    new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"));
            }*/
            try
            {
                JObject jsonObject = new JObject();
                jsonObject.Add("waypointID", waypointID);

                var response = await App.client.PutAsync(uri + "/" + teamID + "/" + adventureID,
                    new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
        }
    }
}
