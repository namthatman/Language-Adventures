using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for Adventure table
	public class Adventure
	{
        [PrimaryKey]
		public int AdventureID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }
		//The type of CoverImage is subject to change later
		//here assume we will be using the url of the image
		public string CoverImage { get; set; }
        [ForeignKey("Waypoint")]
        public int? WaypointID { get; set; }
        public virtual Waypoint Waypoint { get; set; }
	}
}
