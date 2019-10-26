using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for newAdventureMap table, another version of AdventureMap to be displayed by views
    public class newAdventureMap
	{
        [Key]
        public int AdventureMapID { get; set; }
		public int FromWaypointID { get; set; }       
        public int ToWaypointID { get; set; }
        [ForeignKey("FromWaypointID")]
        public virtual Waypoint FromWaypoint { get; set; }
        [ForeignKey("ToWaypointID")]
        public virtual Waypoint ToWaypoint { get; set; }
    }
}
