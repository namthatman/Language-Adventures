using GameControllerData;
using GameControllerData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameControllerServices
{
    public class GameControllerTeamService : IGameControllerTeam
    {
        private GameControllerContext _context;

        public GameControllerTeamService(GameControllerContext context)
        {
            _context = context;
        }

        public IEnumerable<Team> GetAll()
        {
            return _context.Team;
        }

        public Team GetById(int id)
        {
            return _context.Team
                .FirstOrDefault(team => team.TeamId == id);
        }
    }
}
