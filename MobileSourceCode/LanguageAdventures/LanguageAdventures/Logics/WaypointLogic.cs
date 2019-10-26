using LanguageAdventures.Models;
using LanguageAdventures.URLs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LanguageAdventures.Logics
{
    // handles logic for sending http request to web server via api uri
    // requests for Waypoint stuff
    class WaypointLogic
    {
        // expects web server to return the corresponding waypoint object
        public async static Task<Waypoint> GetWaypoint(int waypointID) // TODO: improve by using try-catch to handle exceptions
        {
            Waypoint waypoint = new Waypoint();
            var uri = new Uri(URL.ZONE + URL.WAYPOINT);
            /*using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri + "/" + waypointID);
                var json = await response.Content.ReadAsStringAsync();
                waypoint = JsonConvert.DeserializeObject<Waypoint>(json);
            }*/
            try
            {
                var response = await App.client.GetAsync(uri + "/" + waypointID);
                var json = await response.Content.ReadAsStringAsync();
                waypoint = JsonConvert.DeserializeObject<Waypoint>(json);
            }
            catch (Exception ex)
            {
                //await App.Current.MainPage.DisplayAlert("Warning!", "No internet connection", "Ok");
            }
            return waypoint;
        }
    }
}
