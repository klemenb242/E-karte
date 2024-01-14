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
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    public class EventsController : Controller
    {
        private readonly EkarteContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public EventsController(EkarteContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var ekarteContext = _context.Events.Include(Event => Event.Performer).Include(Event => Event.Venue);
            return View(await ekarteContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(Event => Event.Performer)
                .Include(Event => Event.Venue)
                .Include(Event => Event.Tickets)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Tickets/5
        /*public IActionResult Tickets(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }
            
            var @eventWithTickets = _context.Events
                .Include(e => e.Tickets)
                .FirstOrDefault(e => e.EventID == id);

            if (@eventWithTickets == null)
            {
                return NotFound();
            }

            return View(@eventWithTickets);
        }*/


        // GET: Events/Create
        [Authorize]

        public IActionResult Create()
        {
            ViewData["PerformerID"] = new SelectList(_context.Performers, "PerformerID", "Name");
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        

        public async Task<IActionResult> Create([Bind("EventID,Title,Date,UserID,VenueID,PerformerID,DateCreated,DateEdited")] Event @event)
        {
            var currentUser=await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                @event.User=currentUser;
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PerformerID"] = new SelectList(_context.Performers, "PerformerID", "Name", @event.PerformerID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", @event.VenueID);
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["PerformerID"] = new SelectList(_context.Performers, "PerformerID", "Name", @event.PerformerID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", @event.VenueID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("EventID,Title,Date,UserID,VenueID,PerformerID,DateCreated,DateEdited")] Event @event)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
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
            ViewData["PerformerID"] = new SelectList(_context.Performers, "PerformerID", "Name", @event.PerformerID);
            ViewData["VenueID"] = new SelectList(_context.Venues, "VenueID", "Name", @event.VenueID);
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(Event => Event.Performer)
                .Include(Event => Event.Venue)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'EkarteContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.EventID == id);
        }
    }
}
