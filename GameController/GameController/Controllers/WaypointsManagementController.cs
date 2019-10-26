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
    public class WaypointsManagementController : Controller
    {
        private readonly GameControllerContext _context;

        public WaypointsManagementController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: WaypointsManagement
        public async Task<IActionResult> Index(int? id)
        {
            var gameControllerContext = _context.Waypoint.Include(w => w.Adventure).Include(e => e.Challenge);
            if (id != null)
            {
                gameControllerContext = _context.Waypoint.Where(w => w.AdventureID == id).Include(w => w.Adventure).Include(e => e.Challenge);
            }
            if (gameControllerContext == null)
            {
                return NotFound();
            }
            return View(await gameControllerContext.ToListAsync());
        }

        // GET: WaypointsManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waypoint = await _context.Waypoint
                .Include(w => w.Challenge)
                .FirstOrDefaultAsync(m => m.WaypointID == id);
            if (waypoint == null)
            {
                return NotFound();
            }

            return View(waypoint);
        }

        // GET: WaypointsManagement/Create
        public IActionResult Create()
        {
            ViewData["AdventureID"] = new SelectList(_context.Adventure, "AdventureID", "AdventureID");
            //ViewData["ChallengeID"] = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID");
            var list = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID").ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["ChallengeID"] = list;
            return View();
        }

        // POST: WaypointsManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WaypointID,Longitude,Latitude,Content,ChallengeID,AdventureID")] Waypoint waypoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(waypoint);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "WaypointsManagement/Index/" + waypoint.AdventureID);
            }
            ViewData["AdventureID"] = new SelectList(_context.Adventure, "AdventureID", "AdventureID", waypoint.AdventureID);
            //ViewData["ChallengeID"] = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID", waypoint.ChallengeID);
            var list = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID", waypoint.ChallengeID).ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["ChallengeID"] = list;
            return View(waypoint);
        }

        // GET: WaypointsManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waypoint = await _context.Waypoint.FindAsync(id);
            if (waypoint == null)
            {
                return NotFound();
            }
            //ViewData["ChallengeID"] = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID", waypoint.ChallengeID);
            var list = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID", waypoint.ChallengeID).ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["ChallengeID"] = list;
            ViewData["AdventureID"] = new SelectList(_context.Adventure, "AdventureID", "AdventureID", waypoint.AdventureID);
            return View(waypoint);
        }

        // POST: WaypointsManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WaypointID,Longitude,Latitude,Content,ChallengeID,AdventureID")] Waypoint waypoint)
        {
            if (id != waypoint.WaypointID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(waypoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaypointExists(waypoint.WaypointID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "WaypointsManagement", new { id = waypoint.AdventureID });
            }
            //ViewData["ChallengeID"] = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID", waypoint.ChallengeID);
            var list = new SelectList(_context.Challenge, "ChallengeID", "ChallengeID", waypoint.ChallengeID).ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["ChallengeID"] = list;
            return View(waypoint);
        }

        // GET: WaypointsManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waypoint = await _context.Waypoint
                .Include(w => w.Challenge)
                .FirstOrDefaultAsync(m => m.WaypointID == id);
            if (waypoint == null)
            {
                return NotFound();
            }

            return View(waypoint);
        }

        // POST: WaypointsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waypoint = await _context.Waypoint.FindAsync(id);
            var savedAdventureID = waypoint.AdventureID;
            _context.Waypoint.Remove(waypoint);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "WaypointsManagement", new { id = savedAdventureID });
        }

        private bool WaypointExists(int id)
        {
            return _context.Waypoint.Any(e => e.WaypointID == id);
        }
    }
}
