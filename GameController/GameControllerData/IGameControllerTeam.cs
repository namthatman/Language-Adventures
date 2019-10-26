using GameControllerData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameControllerData
{
    public interface IGameControllerTeam
    {
        IEnumerable<Team> GetAll();

        Team GetById(int id);
    }
}
