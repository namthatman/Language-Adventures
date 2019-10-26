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
    public class MessagesManagementController : Controller
    {
        private readonly GameControllerContext _context;

        public MessagesManagementController(GameControllerContext context)
        {
            _context = context;
        }

        // GET: MessagesManagement
        public async Task<IActionResult> Index()
        {
            var gameControllerContext = _context.Message.Include(m => m.Team);
            return View(await gameControllerContext.ToListAsync());
        }

        // GET: MessagesManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Team)
                .FirstOrDefaultAsync(m => m.MessageID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: MessagesManagement/Create
        public IActionResult Create()
        {
           
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId");
            ViewData["TeamName"] = new SelectList(_context.Team, "Name", "Name");
            return View();
        }

        // POST: MessagesManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageID,Time,MessageContent,TeamID")] Message message, String teamName)
        {
            var team = _context.Team.Where(t => t.Name == teamName).FirstOrDefault();
            message.TeamID = team.TeamId;
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId", message.TeamID);
            return View(message);
        }

        // GET: MessagesManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId", message.TeamID);
            return View(message);
        }

        // POST: MessagesManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageID,Time,MessageContent,TeamID")] Message message)
        {
            if (id != message.MessageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageID))
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
            ViewBag.CurrentTime = DateTime.Now;
            return View(message);
        }

        // GET: MessagesManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Message
                .Include(m => m.Team)
                .FirstOrDefaultAsync(m => m.MessageID == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: MessagesManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Message.FindAsync(id);
            _context.Message.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
            return _context.Message.Any(e => e.MessageID == id);
        }
    }
}
