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
     * API for team's location 
     */
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public LocationController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/Location
        [HttpGet]
        public IEnumerable<Team> GetTeam()
        {
            return _context.Team;
        }
        //Handler for Get request with id
        // GET: api/Location/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var team = await _context.Team.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            var result = new OkObjectResult(new { message = "200 OK", team.Longtitude, team.Latitude });
            return result;
        }
        //Handler for Put request
        // PUT: api/Location/5
        [HttpPut("{id}/{longtitude}|{latitude}")]
        public async Task<IActionResult> PostTeamLocation([FromRoute] int id, [FromRoute] double longtitude, [FromRoute] double latitude)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Team ExistingTeam = _context.Team.Where(team => team.TeamId == id).FirstOrDefault();

            if (ExistingTeam != null)
            {
                ExistingTeam.Longtitude = longtitude;
                ExistingTeam.Latitude = latitude;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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
        //Check if Team with this id exists
        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}