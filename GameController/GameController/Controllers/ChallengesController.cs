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
     * API for Challenges model
     */
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public ChallengesController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/Challenges
        [HttpGet]
        public IEnumerable<Challenge> GetChallenge()
        {
            return _context.Challenge;
        }
        //Handler for Get request with id
        // GET: api/Challenges/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChallenge([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var challenge = await _context.Challenge.FindAsync(id);

            if (challenge == null)
            {
                return NotFound();
            }

            return Ok(challenge);
        }
    }
}