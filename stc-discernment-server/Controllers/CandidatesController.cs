using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using stc_discernment_server.Models;
using Microsoft.EntityFrameworkCore;

namespace stc_discernment_server.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase {

        private readonly AppDbContext _context;
        public CandidatesController(AppDbContext context) { _context = context; }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> List() {
            return await _context.Candidates.ToListAsync();
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> Get(int id) {
            var candidate = await _context.Candidates.FindAsync(id);
            return candidate != null
                    ? Ok(candidate)
                    : NotFound();
        }

        // POST: api/Candidates
        [HttpPost]
        public async Task<ActionResult<Candidate>> Post(Candidate candidate) {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return Ok(candidate);
        }

        // PUT: api/Candidates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Candidate candidate) {
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
                return NotFound();
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
