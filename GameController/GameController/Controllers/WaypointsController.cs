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
     * API for Waypoints model
     */
    [Route("api/[controller]")]
    [ApiController]
    public class WaypointsController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public WaypointsController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/Waypoints
        [HttpGet]
        public IEnumerable<Waypoint> GetWaypoint()
        {
            return _context.Waypoint;
        }
        //Handler for Get request with id
        // GET: api/Waypoints/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWaypoint([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var waypoint = await _context.Waypoint.FindAsync(id);

            if (waypoint == null)
            {
                return NotFound();
            }

            return Ok(waypoint);
        }
    }
}