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
     * Controller for StudentsManagement pages
     */
    public class StudentsManagementController : Controller
    {
        private readonly GameControllerContext _context;

        public StudentsManagementController(GameControllerContext context)
        {
            _context = context;
        }
        //Logic for index page
        // GET: StudentsManagement
        public async Task<IActionResult> Index(int? id)
        {
            var gameControllerContext = _context.Student.Include(s => s.Team);
            if (id != null)
            {
                gameControllerContext = _context.Student.Where(s => s.TeamID == id).Include(s => s.Team);
            }
            if (gameControllerContext == null)
            {
                return NotFound();
            }
            return View(await gameControllerContext.ToListAsync());
        }
        //Logic for detail page
        // GET: StudentsManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        //Logic for create page
        // GET: StudentsManagement/Create
        public IActionResult Create()
        {
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId");
            return View();
        }
        //Post request from create page
        // POST: StudentsManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,TeamID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index) +"/"+ student.TeamID);
            }
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId", student.TeamID);
            return View(student);
        }
        //Logic for edit page
        // GET: StudentsManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId", student.TeamID);
            return View(student);
        }
        //Post request from edit page
        // POST: StudentsManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,TeamID")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index) + "/" + student.TeamID);
            }
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamId", "TeamId", student.TeamID);
            return View(student);
        }
        //Logic for delete page
        // GET: StudentsManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        //Post request from delete page
        // POST: StudentsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index) + "/" + student.TeamID);
        }
        //Check if Student with this id exists
        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}
