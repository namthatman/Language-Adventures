using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageAdventures.Models
{
    public class Message
    {
        public int messageID { get; set; }
        public DateTime time { get; set; }
        public string messageContent { get; set; }
        public int teamID { get; set; }

        public Message()
        {

        }
    }
}
