using System;
using System.Collections.Generic;
using System.Text;

namespace GameControllerData.Models
{
    public interface IGameControllerAdventureSession
    {
        IEnumerable<Session> GetAll();

        Session GetByTeamId(int id);
       
    }
}
