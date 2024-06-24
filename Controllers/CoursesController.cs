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
    public class CoursesController : Controller
    {
        private readonly UniversityDbContext _context;

        public CoursesController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var universityDbContext = _context.Courses.Include(c => c.Faculty).Include(c => c.Lecturer);
            return View(await universityDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Faculty)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Courseid == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultyid");
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Courseid,Facultyid,Lecturerid,Coursename,Credits,Tuitionfee,Ects")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultyid", course.Facultyid);
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid", course.Lecturerid);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultyid", course.Facultyid);
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid", course.Lecturerid);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Courseid,Facultyid,Lecturerid,Coursename,Credits,Tuitionfee,Ects")] Course course)
        {
            if (id != course.Courseid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Courseid))
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
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultyid", course.Facultyid);
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid", course.Lecturerid);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Faculty)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Courseid == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Courseid == id);
        }
    }
}
