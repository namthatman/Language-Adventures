using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageAdventures.Models
{
    class Session
    {
        //TODO add sessionID when it is in the api
        public int sessionID { get; set; }
        public int TeamID { get; set; }
        public int AdventureID { get; set; }
        public int WaypointID { get; set; }

        public Session()
        {

        }


    }
}
