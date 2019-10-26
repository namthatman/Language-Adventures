using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageAdventures.Models
{
    // represents the object of the team
    // stores the data retrieve from the web server
    public class Team
    {
        public int teamID { get; set; } // id of the team
        public string name { get; set; } // name of the team

        public Team()
        {

        }
    }
}
