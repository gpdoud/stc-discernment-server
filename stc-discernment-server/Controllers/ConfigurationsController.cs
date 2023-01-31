using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using stc_discernment_server.Models;

namespace stc_discernment_server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase {

        private readonly AppDbContext _context;

        public ConfigurationsController(AppDbContext context) {
            _context = context;
        }

        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Configuration>>> GetAll() {
            return await _context.Configurations.ToListAsync();
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<Configuration>> GetKey(string key) {
            var sysctrl = await _context.Configurations.FindAsync(key);
            if (sysctrl == null) return NotFound();
            return sysctrl;
        }

        [HttpGet("search/{partKey}")]
        public async Task<ActionResult<IEnumerable<Configuration>>> GetPartialKey(string partKey) {
            return await _context.Configurations.Where(x => x.KeyValue.StartsWith(partKey)).ToListAsync();
        }

        [HttpGet("keys/{keyList}")]
        public async Task<ActionResult<IEnumerable<Configuration>>> GetKeys(string keyList) {
            var keys = keyList.Split(','); // split keys with comma separator
            var configs = new List<Configuration>();
            foreach (var key in keys) {
                var config = await GetKey(key);
                if (config.Value == null) continue;
                configs.Add(config.Value);
            }
            return configs;
        }

        [HttpPost()]
        public async Task<IActionResult> AddUpdateKey(Configuration sysctrl) {
            try {
                // if key exists, update it
                var sc = await _context.Configurations.FindAsync(sysctrl.KeyValue);
                if (sc == null) { // doesn't exist; add it
                    sysctrl.Created = DateTime.Now.ToUniversalTime();
                    _context.Configurations.Add(sysctrl);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(sc);
                }
                // else add the key/value
                sc.DataValue = sysctrl.DataValue;
                sc.Note = sysctrl.Note;
                sc.System = sysctrl.System;
                sc.Active = sysctrl.Active;
                sc.Updated = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
                return new OkObjectResult(sc);
            } catch (Exception ex) {
                return new JsonResult(ex);
            }
        }

        [HttpPost("delete/{key}")]
        public async Task<ActionResult<Configuration>> PostRemove(string key) {
            return await Remove(key);
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<Configuration>> Remove(string key) {
            try {
                var cfg = await _context.Configurations.FindAsync(key);
                if (cfg == null) return NotFound();
                _context.Configurations.Remove(cfg);
                await _context.SaveChangesAsync();
                return cfg;
            } catch (Exception ex) {
                return new JsonResult(ex);
            }
        }

        [HttpGet("/")]
        public async Task<IActionResult> GetStatus() {
            try {
                var Configs = await _context.Configurations.Where(x => x.System).ToArrayAsync();
                return new JsonResult(Configs, jsonOptions);
            } catch (Exception ex) {
                return new JsonResult(ex);
            }
        }

    }
}
