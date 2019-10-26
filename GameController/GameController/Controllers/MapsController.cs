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
    public class MapsController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public MapsController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: api/Maps
        [HttpGet]
        public IEnumerable<newAdventureMap> GetnewAdventureMaps()
        {
            return _context.newAdventureMaps;
        }

        // GET: api/Maps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetnewAdventureMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAdventureMap = _context.newAdventureMaps.Where(am => am.FromWaypointID == id);

            if (newAdventureMap == null)
            {
                return NotFound();
            }

            return Ok(newAdventureMap);
        }

    }
}