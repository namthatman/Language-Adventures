using GameControllerData;
using GameControllerData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameControllerServices
{

    public class GameControllerAdventureSessionService : IGameControllerAdventureSession
    {
        private GameControllerContext _context;
        public GameControllerAdventureSessionService(GameControllerContext context)
        {
            _context = context;
        }
        public IEnumerable<Session> GetAll()
        {
            return _context.Session;
        }

        public Session  GetByTeamId(int id)
        {
            return _context.Session.Where(a => a.TeamID == id)
                .FirstOrDefault();
        }

    }
}
