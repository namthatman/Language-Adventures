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
    // requests for Message stuff
    class MessageLogic
    {
        // expects web server to return list of all messages sent to the team
        public async static Task<List<Message>> GetMessages(int teamID)
        {
            List<Message> messages = new List<Message>();
            try
            {
                var uri = new Uri(URL.ZONE + URL.MESSAGES + teamID);
                var response = await App.client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                messages = JsonConvert.DeserializeObject<List<Message>>(json);
            }
            catch (Exception ex) { }
            return messages;

        }
    }
}
