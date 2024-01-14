using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/Performer")]
    [ApiController]
    public class PerformersApiController : ControllerBase
    {
        private readonly EkarteContext _context;

        public PerformersApiController(EkarteContext context)
        {
            _context = context;
        }

        // GET: api/PerformersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performer>>> GetPerformers()
        {
            return await _context.Performers.ToListAsync();
        }

        // GET: api/PerformersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Performer>> GetPerformer(int id)
        {
            var performer = await _context.Performers.FindAsync(id);

            if (performer == null)
            {
                return NotFound();
            }

            return performer;
        }

        // PUT: api/PerformersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformer(int id, Performer performer)
        {
            if (id != performer.PerformerID)
            {
                return BadRequest();
            }

            _context.Entry(performer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PerformersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Performer>> PostPerformer(Performer performer)
        {
            _context.Performers.Add(performer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformer", new { id = performer.PerformerID }, performer);
        }

        // DELETE: api/PerformersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformer(int id)
        {
            var performer = await _context.Performers.FindAsync(id);
            if (performer == null)
            {
                return NotFound();
            }

            _context.Performers.Remove(performer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerformerExists(int id)
        {
            return _context.Performers.Any(e => e.PerformerID == id);
        }
    }
}
