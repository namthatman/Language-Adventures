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
     * API for Messages model
     */
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public MessagesController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/Messages
        [HttpGet]
        public IEnumerable<Message> GetMessage()
        {
            return _context.Message;
        }
        //Handler for Get request with id
        // GET: api/Messages/5
        [HttpGet("{teamID}")]
        public async Task<IActionResult> GetMessage([FromRoute] int teamID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = _context.Message.Where(mes => mes.TeamID == teamID).ToList();

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }
    }
}