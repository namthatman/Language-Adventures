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
     * API for Adventure model
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AdventuresController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public AdventuresController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/Adventures
        [HttpGet]
        public IEnumerable<Adventure> GetAdventure()
        {
            return _context.Adventure;
        }
        //Handler for Get request with id
        // GET: api/Adventures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdventure([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adventure = await _context.Adventure.FindAsync(id);

            if (adventure == null)
            {
                return NotFound();
            }
            var result = new OkObjectResult(new { message = "200 OK", adventure, adventure.WaypointID });
            return result;
        }
    }
}