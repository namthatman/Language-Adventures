using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageAdventures.Models
{
    // represents the object of an adventure map
    // stores the data retrieve from the web server
    class AdventureMap
    {
        public int adventureMapID { get; set; }// map id

        public int fromWaypointID { get; set; } // id of this current waypoint

        public int toWaypointID { get; set; } // id of the destination waypoint
        

        public AdventureMap (){

        }
    }
}
