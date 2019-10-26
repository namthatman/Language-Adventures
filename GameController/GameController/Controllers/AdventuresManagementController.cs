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
    /*
     * Controller for AdventuresManagement pages
     */
    public class AdventuresManagementController : Controller
    {
        private readonly GameControllerContext _context;

        public AdventuresManagementController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: AdventuresManagement
        public async Task<IActionResult> Index()
        {
            var gameControllerContext = _context.Adventure.Include(a => a.Waypoint);
            return View(await gameControllerContext.ToListAsync());
        }

        // GET: AdventuresManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.Adventure
                .Include(a => a.Waypoint)
                .FirstOrDefaultAsync(m => m.AdventureID == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // GET: AdventuresManagement/Create
        public IActionResult Create()
        {
            //ViewData["WaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID");
            var list = new SelectList(_context.Waypoint, "WaypointID", "WaypointID").ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["WaypointID"] = list;
            return View();
        }

        // POST: AdventuresManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdventureID,Title,Description,CoverImage,WaypointID")] Adventure adventure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adventure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["WaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", adventure.WaypointID);
            var list = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", adventure.WaypointID).ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["WaypointID"] = list;
            return View(adventure);
        }

        // GET: AdventuresManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.Adventure.FindAsync(id);
            if (adventure == null)
            {
                return NotFound();
            }
            //ViewData["WaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", adventure.WaypointID);
            var list = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", adventure.WaypointID).ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["WaypointID"] = list;
            return View(adventure);
        }

        // POST: AdventuresManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdventureID,Title,Description,CoverImage,WaypointID")] Adventure adventure)
        {
            if (id != adventure.AdventureID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adventure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventureExists(adventure.AdventureID))
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
            //ViewData["WaypointID"] = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", adventure.WaypointID);
            var list = new SelectList(_context.Waypoint, "WaypointID", "WaypointID", adventure.WaypointID).ToList();
            list.Insert(0, new SelectListItem() { Value = null, Text = null });
            ViewData["WaypointID"] = list;
            return View(adventure);
        }

        // GET: AdventuresManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.Adventure
                .Include(a => a.Waypoint)
                .FirstOrDefaultAsync(m => m.AdventureID == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // POST: AdventuresManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adventure = await _context.Adventure.FindAsync(id);
            _context.Adventure.Remove(adventure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Check if Adventure with this id exists
        private bool AdventureExists(int id)
        {
            return _context.Adventure.Any(e => e.AdventureID == id);
        }
    }
}
