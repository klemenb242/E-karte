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
    public class PurchaseController : Controller
    {
        private readonly EkarteContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public PurchaseController(EkarteContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Purchase/BuyTicket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTicket(int eventId, int ticketId)
        {
            // Assuming you have the necessary logic to retrieve the user ID
            
            var currentUser=await _usermanager.GetUserAsync(User);


            // Create a new purchase record
            var newPurchase = new Purchase
            {
                TicketID = ticketId,
                UserID = currentUser.Id,
                // You may need to set other properties based on your application logic
            };

            _context.Purchases.Add(newPurchase);
            await _context.SaveChangesAsync();

            // Store success message in TempData
            TempData["SuccessMessage"] = "Ticket purchased successfully!";

            // Redirect to the event details page or any other desired page
            return RedirectToAction("Details", "Events", new { id = eventId });
        }

        // GET: Purchase
        public async Task<IActionResult> Index()
        {
            var ekarteContext = _context.Purchases.Include(p => p.Ticket).Include(p => p.User);
            return View(await ekarteContext.ToListAsync());
        }

        // GET: Purchase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Ticket)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchase/Create
        public IActionResult Create()
        {
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID");
            ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseID,TicketID,UserID")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID", purchase.TicketID);
            ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", purchase.UserID);
            return View(purchase);
        }

        // GET: Purchase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID", purchase.TicketID);
            ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", purchase.UserID);
            return View(purchase);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseID,TicketID,UserID")] Purchase purchase)
        {
            if (id != purchase.PurchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.PurchaseID))
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
            ViewData["TicketID"] = new SelectList(_context.Tickets, "TicketID", "TicketID", purchase.TicketID);
            ViewData["UserID"] = new SelectList(_context.ApplicationUsers, "Id", "Id", purchase.UserID);
            return View(purchase);
        }

        // GET: Purchase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Ticket)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PurchaseID == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'EkarteContext.Purchases'  is null.");
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
          return _context.Purchases.Any(e => e.PurchaseID == id);
        }
    }
}
