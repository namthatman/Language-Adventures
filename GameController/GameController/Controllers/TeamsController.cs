using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameControllerData;
using GameControllerData.Models;
using GameController.ViewModel;

namespace GameController.Controllers
{
    /*
     * Controller for Teams pages
     */
    public class TeamsController : Controller
    {
        private readonly GameControllerContext _context;

        private readonly IGameControllerTeam _teams;

        private readonly IGameControllerWaypoint _waypoints;
        private readonly IGameControllerAdventureSession _sessions;
        public TeamsController(GameControllerContext context, IGameControllerTeam teams, IGameControllerWaypoint waypoints, IGameControllerAdventureSession sessions)
        {
            _context = context;
            _teams = teams;
            _waypoints = waypoints;
            _sessions = sessions;
        }
        //Logic for index page
        // GET: Teams
        public IActionResult Index()
        {
            ViewModelTeamWaypoint vmTeam = new ViewModelTeamWaypoint();
            vmTeam.AllTeam = _teams.GetAll().ToList();
            vmTeam.Team = _teams.GetById(1);
            vmTeam.AllWaypoint = _waypoints.GetAll().ToList();
            return View(vmTeam);
        }
        //Logic for detail page
        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var id2 = id.GetValueOrDefault();
            ViewModelTeamWaypoint vmTeam = new ViewModelTeamWaypoint();
            
            vmTeam.Team = _teams.GetById(id2);
            var adventure = _sessions.GetByTeamId(id2);

            if(adventure == null)
            {
                vmTeam.AllWaypoint = null;
                return View(vmTeam);
            }
            
            vmTeam.AllWaypoint = _waypoints.GetAll().Where(w => w.AdventureID == adventure.AdventureID).ToList();
            
            

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(vmTeam);
        }
        //Logic for create page
        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }
        //Logic for edit page
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }
        //Post request from edit page
        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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
            return View(team);
        }
        //Logic for delete page
        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
        //Post request from delete page
        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Team.FindAsync(id);
            _context.Team.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Check if Team with this id exists
        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.TeamId == id);
        }
    }
}
