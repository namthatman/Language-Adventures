using System;
using System.Collections.Generic;
using System.Text;


namespace LanguageAdventures.Models
{
    // represents the object of an adventure
    // stores the data retrieve from the web server
    public class Adventure
    {
        public int adventureID { get; set; } // id of this adventure
        public string title { get; set; } // title of this adventure
        public string description { get; set; } // description of this adventure
        public string coverImage { get; set; } // cover image of this adventure
        public int? waypointID { get; set; } // id of the first waypoint of this adventure

        public Adventure()
        {

        }

        public override string ToString()
        {
            return this.title;
        }
    }
}