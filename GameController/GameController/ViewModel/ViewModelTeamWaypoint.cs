using GameControllerData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameController.ViewModel
{
    public class ViewModelTeamWaypoint
    {
        public List<Team> AllTeam { get; set; }
        public List<Waypoint> AllWaypoint { get; set; }
        public List<AdventureSession> AdventureSessions { get; set; }
        public Team Team { get; set; }
        public Waypoint Waypoint { get; set; }
        public AdventureSession AdventureSession { get; set; }


        
    }
}
