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
    /*
     * API for AdventureSessions model
     */

    [Route("api/[controller]")]
    [ApiController]
    public class AdventureSessionsController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public AdventureSessionsController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/AdventureSessions
        [HttpGet]
        public IEnumerable<AdventureSession> GetAdventureSession()
        {
            return _context.AdventureSession;
        }
        //Handler for Get request with id
        // GET: api/AdventureSessions/5
        [HttpGet("{teamID}/{adventureID}")]
        public async Task<IActionResult> GetAdventureSession([FromRoute] int teamID, [FromRoute] int adventureID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adventureSession = await _context.AdventureSession.Where(ads => ads.TeamID == teamID)
                .Where(ads => ads.AdventureID == adventureID).FirstOrDefaultAsync();

            if (adventureSession == null)
            {
                return NotFound();
            }

            return Ok(adventureSession);
        }
        //Handler for Put request
        // PUT: api/AdventureSessions/5
        [HttpPut("{teamID}/{adventureID}")]
        public async Task<IActionResult> PutAdventureSession([FromRoute] int teamID, [FromRoute] int adventureID, [FromBody] AdventureSession adventureSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AdventureSession ExistingAdventureSession = await _context.AdventureSession.Where(ads => ads.TeamID == teamID)
                .Where(ads => ads.AdventureID == adventureID).FirstOrDefaultAsync();

            if (ExistingAdventureSession != null)
            {
                ExistingAdventureSession.WaypointID = adventureSession.WaypointID;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ExistingAdventureSession == null)
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
        //Handler for Post request
        // POST: api/AdventureSessions
        [HttpPost]
        public async Task<IActionResult> PostAdventureSession([FromBody] AdventureSession adventureSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AdventureSession.Add(adventureSession);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdventureSessionExists(adventureSession.TeamID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdventureSession", new { id = adventureSession.TeamID }, adventureSession);
        }
        //Check if AdventureSession with this id exists
        private bool AdventureSessionExists(int id)
        {
            return _context.AdventureSession.Any(e => e.TeamID == id);
        }
    }
}