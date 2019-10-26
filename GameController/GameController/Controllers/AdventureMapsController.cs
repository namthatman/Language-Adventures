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
     * API for AdventureMaps model
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureMapsController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public AdventureMapsController(GameControllerContext context)
        {
            _context = context;
        }

        //Handler for Get request
        // GET: api/AdventureMaps
        [HttpGet]
        public IEnumerable<AdventureMap> GetAdventureMap()
        {
            return _context.AdventureMap;
        }

        //Handler for Get request with specific id
        // GET: api/AdventureMaps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdventureMap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _context.AdventureMap.Where(am => am.FromWaypointID == id);

            await _context.SaveChangesAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}