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

        // GET: api/Parishioners/Candidates
        [HttpGet("candidates")]
        public async Task<ActionResult<IEnumerable<Parishioner>>> GetCandidates() {
            return await _context.Parishioners
                                .Include(x => x.Caller)
                                .Where(x => x.Ministry == "Parishioner")
                                .OrderBy(x => x.Caller.Lastname)
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

        class Caller {
            public static int ParishionersPerCaller { get; set; }
            public int Id { get; set; } // caller parishioner id
            public List<int> ParishionerIds { get; set; } = new List<int>();
            public string CallerName { get; set; } = string.Empty;
            public int ParishionersAssignedToCall { get; set; }
        }

        [HttpPut("assigncallers")]
        public async Task<IActionResult> AssignCallersToParishioners() {
            // get list of candidates
            var candidates = await _context.Parishioners.Where(x => x.Ministry == "Parishioner").ToListAsync();
            // get list of callers
            var callers = from p in _context.Parishioners
                          where p.IsCaller
                          select new Caller {
                              Id = p.Id, 
                              CallerName = p.Lastname,
                              ParishionersAssignedToCall = 0
                          };
            List<Caller> callersList = new List<Caller>(callers.ToArray());
            Caller.ParishionersPerCaller = ( candidates.Count() / callers.Count() ) + 1;
            // count all candidates already assigned to callers
            foreach(var caller in callersList.ToArray()) {
                caller.ParishionersAssignedToCall = _context.Parishioners.Count(x => x.CallerId == caller.Id);
            }
            // sort queue asc by parishioners assigned to call
            // callers with fewest manually assigned calls
            // will get parishioners to be called first
            var callersQueue = new Queue<Caller>(callersList.OrderBy(x => x.ParishionersAssignedToCall).ToArray());
            // get the caller from the front of the queue
            var assignedCaller = GetNextCaller(callersQueue);
            foreach (var candidate in candidates) {
                // if this candidate is not assigned a caller
                if(candidate.CallerId is not null) {
                    continue; // skip it
                }
                // assigned the candidate to the assignedCaller
                candidate.CallerId = assignedCaller.Id;
                // record the candidate's Id in the caller's parishionerIds
                assignedCaller.ParishionerIds.Add(candidate.Id);
                // increment the assigned caller's parishioners assigned to call
                assignedCaller.ParishionersAssignedToCall++;
                // put the caller back in the queue
                callersQueue.Enqueue(assignedCaller);
                assignedCaller = GetNextCaller(callersQueue);
            }
            // update the DB with the caller assignments
            foreach(var caller3 in callersQueue.ToArray()) {
                var pList = from p in _context.Parishioners.AsEnumerable()
                            join c in caller3.ParishionerIds.AsEnumerable()
                                on p.Id equals c
                            select p;
                foreach(var p in pList) {
                    p.CallerId = caller3.Id;
                }
                await _context.SaveChangesAsync();
            }
            
            return Ok();
        }

        private Caller GetNextCaller(Queue<Caller> queue) {
            var caller = queue.Dequeue();
            while(caller.ParishionersAssignedToCall >= Caller.ParishionersPerCaller) {
                queue.Enqueue(caller);
                caller = queue.Dequeue();
            }
            return caller;
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
