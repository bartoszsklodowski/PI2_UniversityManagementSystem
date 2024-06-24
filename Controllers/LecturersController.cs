using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Core;
using University.Models;

namespace University.Controllers
{
    [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
    public class LecturersController : Controller
    {
        private readonly UniversityDbContext _context;

        public LecturersController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: Lecturers
        public async Task<IActionResult> Index()
        {
            var universityDbContext = _context.Lecturers.Include(l => l.Department).Include(l => l.User);
            return View(await universityDbContext.ToListAsync());
        }

        // GET: Lecturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Department)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Lecturerid == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // GET: Lecturers/Create
        public IActionResult Create()
        {
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Departmentid");
            ViewData["Userid"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Lecturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Lecturerid,Userid,Firstname,Lastname,Email,Departmentid")] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Departmentid", lecturer.Departmentid);
            ViewData["Userid"] = new SelectList(_context.AspNetUsers, "Id", "Id", lecturer.Userid);
            return View(lecturer);
        }

        // GET: Lecturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
            {
                return NotFound();
            }
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Departmentid", lecturer.Departmentid);
            ViewData["Userid"] = new SelectList(_context.AspNetUsers, "Id", "Id", lecturer.Userid);
            return View(lecturer);
        }

        // POST: Lecturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Lecturerid,Userid,Firstname,Lastname,Email,Departmentid")] Lecturer lecturer)
        {
            if (id != lecturer.Lecturerid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerExists(lecturer.Lecturerid))
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
            ViewData["Departmentid"] = new SelectList(_context.Departments, "Departmentid", "Departmentid", lecturer.Departmentid);
            ViewData["Userid"] = new SelectList(_context.AspNetUsers, "Id", "Id", lecturer.Userid);
            return View(lecturer);
        }

        // GET: Lecturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturer = await _context.Lecturers
                .Include(l => l.Department)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Lecturerid == id);
            if (lecturer == null)
            {
                return NotFound();
            }

            return View(lecturer);
        }

        // POST: Lecturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer != null)
            {
                _context.Lecturers.Remove(lecturer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.Lecturerid == id);
        }
    }
}
