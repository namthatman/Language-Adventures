using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameControllerData;
using GameControllerData.Models;

namespace GameController.Controllers
{
    public class ChallengesManagementController : Controller
    {
        private readonly GameControllerContext _context;

        public ChallengesManagementController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: ChallengesManagement
        public async Task<IActionResult> Index(int? adventureID, int? challengeID, int? waypointID)
        {
            ViewData["adventureID"] = adventureID;
            ViewData["waypointID"] = waypointID;
            if (challengeID == null)
            {
                if (adventureID == null)
                {

                    return View(await _context.Challenge.ToListAsync());
                }
                else
                {
                    return View(null);
                }
            }

            return View(await _context.Challenge.Where(c => c.ChallengeID == challengeID).ToListAsync());
        }

        // GET: ChallengesManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenge
                .FirstOrDefaultAsync(m => m.ChallengeID == id);
            if (challenge == null)
            {
                return NotFound();
            }

            return View(challenge);
        }
        // Create function in a single waypoint
        // GET: ChallengesManagement/Create
        public IActionResult Create(int? waypointID)
        {
            ViewData["waypointID"] = waypointID;
            return View();
        }

        // Create function in the list of all challenges
        public IActionResult CreateAll()
        {
            return View();
        }

        // POST: ChallengesManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChallengeID,ChallengeType,ChallengeDetail,CorrectAnswer")] Challenge challenge, int? waypointID)
        {
            if (ModelState.IsValid)
            {
                _context.Add(challenge);
                var waypoint = _context.Waypoint.Where(wp => wp.WaypointID == waypointID).FirstOrDefault();
                waypoint.ChallengeID = challenge.ChallengeID;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "WaypointsManagement", new { id = waypoint.AdventureID});
            }
            return View(challenge);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAll([Bind("ChallengeID,ChallengeType,ChallengeDetail,CorrectAnswer")] Challenge challenge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(challenge);
               
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ChallengesManagement");
            }
            return View(challenge);
        }

        // GET: ChallengesManagement/Edit/5
        public async Task<IActionResult> Edit(int? id, int? adventureID, int? waypointID)
        {
            ViewData["adventureID"] = adventureID;
            ViewData["waypointID"] = waypointID;
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenge.FindAsync(id);
            if (challenge == null)
            {
                return NotFound();
            }
            return View(challenge);
        }

        // POST: ChallengesManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChallengeID,ChallengeType,ChallengeDetail,CorrectAnswer")] Challenge challenge, int? adventureID, int? waypointID)
        {
            if (id != challenge.ChallengeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(challenge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChallengeExists(challenge.ChallengeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "ChallengesManagement", new {challengeID = challenge.ChallengeID, adventureID = adventureID, waypointID = waypointID});
            }
            return View(challenge);
        }

        // GET: ChallengesManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenge
                .FirstOrDefaultAsync(m => m.ChallengeID == id);
            if (challenge == null)
            {
                return NotFound();
            }

            return View(challenge);
        }

        // POST: ChallengesManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var challenge = await _context.Challenge.FindAsync(id);
            _context.Challenge.Remove(challenge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChallengeExists(int id)
        {
            return _context.Challenge.Any(e => e.ChallengeID == id);
        }
    }
}
