using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Session table
    public class Session
    {
        [Key]
        public int SessionID { get; set; }
        
        [ForeignKey("Team")]
        public int TeamID { get; set; }

        [ForeignKey("Adventure")]
        public int AdventureID { get; set; }

        [ForeignKey("Waypoint")]
        public int WaypointID { get; set; }

        public virtual Team Team { get; set; }
        public virtual Adventure Adventure { get; set; }
        public virtual Waypoint Waypoint { get; set; }
    }
}
