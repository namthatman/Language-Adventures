using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageAdventures.Models
{
    // represents the object of a waypoint
    // stores the data retrieve from the web server
    public class Waypoint
    {
        public int WaypointId { get; set; } // id of this waypoint
        public double Longitude { get; set; } // longitude of this waypoint
        public double Latitude { get; set; } // latitude of this waypoint
        public string Content { get; set; } // content of this waypoint
        public int? ChallengeId { get; set; } // id of this waypoint's challenge

        public Waypoint()
        {

        }
    }
}
