using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Waypoint table
    public class Waypoint
	{
        [Key]
		public int WaypointID { get; set; }

		public double Longitude { get; set; }

		public double Latitude { get; set; }

		public string Content { get; set; }

        [ForeignKey("Challenge")]
        public int? ChallengeID { get; set; }

        public virtual Challenge Challenge { get; set; }
        [ForeignKey("Adventure")]
        public int? AdventureID { get; set; }
        public virtual Adventure Adventure { get; set; }

	}
}
