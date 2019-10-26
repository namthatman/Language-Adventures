using GameControllerData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameControllerData
{
    public interface IGameControllerWaypoint
    {
        IEnumerable<Waypoint> GetAll();
    }
}
