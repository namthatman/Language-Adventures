using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameControllerData;
using GameControllerData.Models;

namespace GameController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public SessionsController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public IEnumerable<Session> GetSessions()
        {
            return _context.Session;
        }

        // GET: api/Sessions/5
        [HttpGet("{teamID}/{adventureID}")]
        public async Task<IActionResult> GetSession([FromRoute] int teamID, [FromRoute] int adventureID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var session = await _context.Session.Where(s => s.TeamID == teamID)
                .Where(s => s.AdventureID == adventureID).FirstOrDefaultAsync();

            if (session == null)
            {
                return NotFound();
            }

            return Ok(session);
        }

        // PUT: api/Sessions/5
        [HttpPut("{teamID}/{adventureID}")]
        public async Task<IActionResult> PutSession([FromRoute] int teamID, [FromRoute] int adventureID, [FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session ExistingSession = await _context.Session.Where(s => s.TeamID == teamID)
                .Where(s => s.AdventureID == adventureID).FirstOrDefaultAsync();

            if (ExistingSession != null)
            {
                ExistingSession.WaypointID = session.WaypointID;
            }

            //_context.Entry(session).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ExistingSession == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sessions
        [HttpPost]
        public async Task<IActionResult> PostSession([FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Session.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSession", new { id = session.SessionID }, session);
        }

        private bool SessionExists(int id)
        {
            return _context.Session.Any(e => e.SessionID == id);
        }
    }
}