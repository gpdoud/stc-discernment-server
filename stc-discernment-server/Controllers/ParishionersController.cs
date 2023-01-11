using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using stc_discernment_server.Models;

namespace stc_discernment_server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ParishionersController : ControllerBase {
        private readonly AppDbContext _context;

        public ParishionersController(AppDbContext context) {
            _context = context;
        }

        [HttpGet("callers")]
        public async Task<ActionResult<IEnumerable<Parishioner>>> GetCommittee() {
            return await _context.Parishioners
                                .Where(x => x.IsCaller)
                                .OrderBy(x => x.Lastname)
                                .ToListAsync();
        }

        // GET: api/Parishioners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parishioner>>> GetParishioners() {
            return await _context.Parishioners
                                .Include(x => x.Caller)
                                .ToListAsync();
        }

        // GET: api/Parishioners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parishioner>> GetParishioner(int id) {
            var parishioner = await _context.Parishioners
                                            .Include(x => x.Caller)
                                            .SingleOrDefaultAsync(x => x.Id == id);

            if (parishioner == null) {
                return NotFound();
            }

            return parishioner;
        }

        // PUT: api/Parishioners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParishioner(int id, Parishioner parishioner) {
            if (id != parishioner.Id) {
                return BadRequest();
            }
            
            parishioner.Updated = DateTime.Now;
            _context.Entry(parishioner).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ParishionerExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Parishioners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parishioner>> PostParishioner(Parishioner parishioner) {
            parishioner.Created= DateTime.Now;
            _context.Parishioners.Add(parishioner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParishioner", new { id = parishioner.Id }, parishioner);
        }

        // DELETE: api/Parishioners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParishioner(int id) {
            var parishioner = await _context.Parishioners.FindAsync(id);
            if (parishioner == null) {
                return NotFound();
            }

            _context.Parishioners.Remove(parishioner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParishionerExists(int id) {
            return _context.Parishioners.Any(e => e.Id == id);
        }
    }
}
