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
    public class newAdventureMapsController : Controller
    {
        private readonly GameControllerContext _context;

        public newAdventureMapsController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: newAdventureMaps
        public async Task<IActionResult> Index()
        {
            var gameControllerContext = _context.newAdventureMaps.Include(n => n.FromWaypoint).Include(n => n.ToWaypoint);
            return View(await gameControllerContext.ToListAsync());
        }

        // GET: newAdventureMaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newAdventureMap = await _context.newAdventureMaps
                .Include(n => n.FromWaypoint)
                .Include(n => n.ToWaypoint)
                .FirstOrDefaultAsync(m => m.AdventureMapID == id);
            if (newAdventureMap == null)
            {
                return NotFound();
            }

            return View(newAdventureMap);
        }

        // GET: newAdventureMaps/Create
        public IActionResult Create()
        {
            ViewData["FromWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID");
            ViewData["ToWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID");
            return View();
        }

        // POST: newAdventureMaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdventureMapID,FromWaypointID,ToWaypointID")] newAdventureMap newAdventureMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newAdventureMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", newAdventureMap.FromWaypointID);
            ViewData["ToWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", newAdventureMap.ToWaypointID);
            return View(newAdventureMap);
        }

        // GET: newAdventureMaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newAdventureMap = await _context.newAdventureMaps.FindAsync(id);
            if (newAdventureMap == null)
            {
                return NotFound();
            }
            ViewData["FromWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", newAdventureMap.FromWaypointID);
            ViewData["ToWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", newAdventureMap.ToWaypointID);
            return View(newAdventureMap);
        }

        // POST: newAdventureMaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdventureMapID,FromWaypointID,ToWaypointID")] newAdventureMap newAdventureMap)
        {
            if (id != newAdventureMap.AdventureMapID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newAdventureMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!newAdventureMapExists(newAdventureMap.AdventureMapID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", newAdventureMap.FromWaypointID);
            ViewData["ToWaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", newAdventureMap.ToWaypointID);
            return View(newAdventureMap);
        }

        // GET: newAdventureMaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newAdventureMap = await _context.newAdventureMaps
                .Include(n => n.FromWaypoint)
                .Include(n => n.ToWaypoint)
                .FirstOrDefaultAsync(m => m.AdventureMapID == id);
            if (newAdventureMap == null)
            {
                return NotFound();
            }

            return View(newAdventureMap);
        }

        // POST: newAdventureMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newAdventureMap = await _context.newAdventureMaps.FindAsync(id);
            _context.newAdventureMaps.Remove(newAdventureMap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool newAdventureMapExists(int id)
        {
            return _context.newAdventureMaps.Any(e => e.AdventureMapID == id);
        }
    }
}
