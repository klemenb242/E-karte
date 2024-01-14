using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;

namespace web.Controllers
{
    public class PerformerController : Controller
    {
        private readonly EkarteContext _context;

        public PerformerController(EkarteContext context)
        {
            _context = context;
        }

        // GET: Performer
        public async Task<IActionResult> Index()
        {
              return _context.Performers != null ? 
                          View(await _context.Performers.ToListAsync()) :
                          Problem("Entity set 'EkarteContext.Performers'  is null.");
        }

        // GET: Performer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Performers == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers
                .FirstOrDefaultAsync(m => m.PerformerID == id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        // GET: Performer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformerID,Name,Description")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Events");
            }
            return RedirectToAction("Create", "Events");
        }

        // GET: Performer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Performers == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers.FindAsync(id);
            if (performer == null)
            {
                return NotFound();
            }
            return View(performer);
        }

        // POST: Performer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformerID,Name,Description")] Performer performer)
        {
            if (id != performer.PerformerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformerExists(performer.PerformerID))
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
            return View(performer);
        }

        // GET: Performer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Performers == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers
                .FirstOrDefaultAsync(m => m.PerformerID == id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        // POST: Performer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Performers == null)
            {
                return Problem("Entity set 'EkarteContext.Performers'  is null.");
            }
            var performer = await _context.Performers.FindAsync(id);
            if (performer != null)
            {
                _context.Performers.Remove(performer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerExists(int id)
        {
          return (_context.Performers?.Any(e => e.PerformerID == id)).GetValueOrDefault();
        }
    }
}
