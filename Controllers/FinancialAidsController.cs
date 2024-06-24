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
    public class FinancialAidsController : Controller
    {
        private readonly UniversityDbContext _context;

        public FinancialAidsController(UniversityDbContext context)
        {
            _context = context;
        }

        // GET: FinancialAids
        public async Task<IActionResult> Index()
        {
            var universityDbContext = _context.FinancialAids.Include(f => f.Student);
            return View(await universityDbContext.ToListAsync());
        }

        // GET: FinancialAids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAid = await _context.FinancialAids
                .Include(f => f.Student)
                .FirstOrDefaultAsync(m => m.Aidid == id);
            if (financialAid == null)
            {
                return NotFound();
            }

            return View(financialAid);
        }

        // GET: FinancialAids/Create
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Create()
        {
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid");
            return View();
        }

        // POST: FinancialAids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Create([Bind("Aidid,Studentid,Amount,Aidtype,Awardyear")] FinancialAid financialAid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financialAid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", financialAid.Studentid);
            return View(financialAid);
        }

        // GET: FinancialAids/Edit/5
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAid = await _context.FinancialAids.FindAsync(id);
            if (financialAid == null)
            {
                return NotFound();
            }
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", financialAid.Studentid);
            return View(financialAid);
        }

        // POST: FinancialAids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Edit(int id, [Bind("Aidid,Studentid,Amount,Aidtype,Awardyear")] FinancialAid financialAid)
        {
            if (id != financialAid.Aidid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialAid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialAidExists(financialAid.Aidid))
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
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", financialAid.Studentid);
            return View(financialAid);
        }

        // GET: FinancialAids/Delete/5
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAid = await _context.FinancialAids
                .Include(f => f.Student)
                .FirstOrDefaultAsync(m => m.Aidid == id);
            if (financialAid == null)
            {
                return NotFound();
            }

            return View(financialAid);
        }

        // POST: FinancialAids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financialAid = await _context.FinancialAids.FindAsync(id);
            if (financialAid != null)
            {
                _context.FinancialAids.Remove(financialAid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialAidExists(int id)
        {
            return _context.FinancialAids.Any(e => e.Aidid == id);
        }
    }
}
