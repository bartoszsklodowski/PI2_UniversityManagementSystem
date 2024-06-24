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
    public class ClassSchedulesController : Controller
    {
        private readonly UniversityDbContext _context;

        public ClassSchedulesController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: ClassSchedules
        public async Task<IActionResult> Index()
        {
            var universityDbContext = _context.ClassSchedules.Include(c => c.Course).Include(c => c.Lecturer);
            return View(await universityDbContext.ToListAsync());
        }

        // GET: ClassSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedules
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Scheduleid == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }

        // GET: ClassSchedules/Create
        public IActionResult Create()
        {
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid");
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid");
            return View();
        }

        // POST: ClassSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Scheduleid,Courseid,Lecturerid,Roomnumber,Classtime")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", classSchedule.Courseid);
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid", classSchedule.Lecturerid);
            return View(classSchedule);
        }

        // GET: ClassSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedules.FindAsync(id);
            if (classSchedule == null)
            {
                return NotFound();
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", classSchedule.Courseid);
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid", classSchedule.Lecturerid);
            return View(classSchedule);
        }

        // POST: ClassSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Scheduleid,Courseid,Lecturerid,Roomnumber,Classtime")] ClassSchedule classSchedule)
        {
            if (id != classSchedule.Scheduleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassScheduleExists(classSchedule.Scheduleid))
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
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Courseid", classSchedule.Courseid);
            ViewData["Lecturerid"] = new SelectList(_context.Lecturers, "Lecturerid", "Lecturerid", classSchedule.Lecturerid);
            return View(classSchedule);
        }

        // GET: ClassSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSchedule = await _context.ClassSchedules
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Scheduleid == id);
            if (classSchedule == null)
            {
                return NotFound();
            }

            return View(classSchedule);
        }

        // POST: ClassSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classSchedule = await _context.ClassSchedules.FindAsync(id);
            if (classSchedule != null)
            {
                _context.ClassSchedules.Remove(classSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassScheduleExists(int id)
        {
            return _context.ClassSchedules.Any(e => e.Scheduleid == id);
        }
    }
}
