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
     * API for Submission model
     */
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly GameControllerContext _context;

        public SubmissionsController(GameControllerContext context)
        {
            _context = context;
        }
        //Handler for Get request
        // GET: api/Submissions
        [HttpGet]
        public IEnumerable<Submission> GetSubmission()
        {
            return _context.Submission;
        }
        //Handler for Get request with id
        // GET: api/Submissions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmission([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var submission = await _context.Submission.FindAsync(id);

            if (submission == null)
            {
                return NotFound();
            }

            return Ok(submission);
        }
        //Handler for Put request
        // PUT: api/Submissions/5
        [HttpPut("{teamID}/{challengeID}")]
        public async Task<IActionResult> PutSubmission([FromRoute] int teamID, [FromRoute] int challengeID, [FromBody] Submission submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Submission ExistingSubmission = _context.Submission.Where(sub => sub.TeamID == teamID)
                .Where(sub => sub.ChallengeID == challengeID).FirstOrDefault();

            if (ExistingSubmission != null)
            {
                ExistingSubmission.Text = submission.Text;
                ExistingSubmission.Image = submission.Image;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ExistingSubmission == null)
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
        // POST: api/Submissions
        [HttpPost]
        public async Task<IActionResult> PostSubmission([FromBody] Submission submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Submission.Add(submission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubmission", new { id = submission.SubmissionID }, submission);
        }
    }
}