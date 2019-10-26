using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameControllerData.Models
{
    //Model for AdventureMap table, for mapping one waypoint to another
    public class AdventureMap
	{
		public int FromWaypointID { get; set; }       
        public int ToWaypointID { get; set; }
        [ForeignKey("FromWaypointID")]
        public virtual Waypoint FromWaypoint { get; set; }
        [ForeignKey("ToWaypointID")]
        public virtual Waypoint ToWaypoint { get; set; }
    }
}
