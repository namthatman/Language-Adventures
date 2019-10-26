using GameControllerData;
using GameControllerData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameControllerServices
{
    public class GameControllerWaypointService : IGameControllerWaypoint
    {
        private GameControllerContext _context;

        public GameControllerWaypointService(GameControllerContext context)
        {
            _context = context;
        }
        public IEnumerable<Waypoint> GetAll()
        {
            return _context.Waypoint;
        }
    }
}
