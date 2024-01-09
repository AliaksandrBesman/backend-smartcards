using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_smartcards.Models;

namespace backend_smartcards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrantingAccessesController : ControllerBase
    {
        private readonly SmartcardsdbContext _context;

        public GrantingAccessesController(SmartcardsdbContext context)
        {
            _context = context;
        }

        // GET: api/GrantingAccesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrantingAccess>>> GetGrantingAccesses()
        {
            return await _context.GrantingAccesses.ToListAsync();
        }

        // GET: api/GrantingAccesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GrantingAccess>> GetGrantingAccess(int id)
        {
            var grantingAccess = await _context.GrantingAccesses.FindAsync(id);

            if (grantingAccess == null)
            {
                return NotFound();
            }

            return grantingAccess;
        }

        // PUT: api/GrantingAccesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrantingAccess(int id, GrantingAccess grantingAccess)
        {
            if (id != grantingAccess.Id)
            {
                return BadRequest();
            }

            _context.Entry(grantingAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrantingAccessExists(id))
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

        // POST: api/GrantingAccesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GrantingAccess>> PostGrantingAccess(GrantingAccess grantingAccess)
        {
            _context.GrantingAccesses.Add(grantingAccess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrantingAccess", new { id = grantingAccess.Id }, grantingAccess);
        }

        // DELETE: api/GrantingAccesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrantingAccess(int id)
        {
            var grantingAccess = await _context.GrantingAccesses.FindAsync(id);
            if (grantingAccess == null)
            {
                return NotFound();
            }

            _context.GrantingAccesses.Remove(grantingAccess);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrantingAccessExists(int id)
        {
            return _context.GrantingAccesses.Any(e => e.Id == id);
        }
    }
}
