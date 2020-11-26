using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [AllowAnonymous]
    [Route("Api/[controller]")]
    [ApiController]
    public class ProfesoretController : ControllerBase
    {
        private readonly WebAPIContext _context;
        private ILogger<Profesoret> _logger;

        public ProfesoretController(WebAPIContext context, ILogger<Profesoret> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Profesoret
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Profesoret>>> GetProfesorets()
        {
            logToConsole(true);
            return await _context.Profesorets.ToListAsync();
        }

        // GET: api/Profesoret/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profesoret>> GetProfesoret(int id)
        {
            var profesoret = await _context.Profesorets.FindAsync(id);

            if (profesoret == null)
            {
                logToConsole(false);
                return NotFound();
            }

            logToConsole(true);
            return profesoret;
        }

        // PUT: api/Profesoret/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProfesoret(int id, Profesoret profesoret)
        {
            if (id != profesoret.Id)
            {
                logToConsole(false);
                return BadRequest();
            }

            _context.Entry(profesoret).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoretExists(id))
                {
                    logToConsole(false);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            logToConsole(true);
            return NoContent();
        }

        // POST: api/Profesoret
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Profesoret>> PostProfesoret(Profesoret profesoret)
        {
            _context.Profesorets.Add(profesoret);
            await _context.SaveChangesAsync();

            logToConsole(true);
            return CreatedAtAction("GetProfesoret", new { id = profesoret.Id }, profesoret);
        }

        // DELETE: api/Profesoret/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Profesoret>> DeleteProfesoret(int id)
        {
            var profesoret = await _context.Profesorets.FindAsync(id);
            if (profesoret == null)
            {
                logToConsole(false);
                return NotFound();
            }

            _context.Profesorets.Remove(profesoret);
            await _context.SaveChangesAsync();

            logToConsole(true);
            return profesoret;
        }

        private bool ProfesoretExists(int id)
        {
            return _context.Profesorets.Any(e => e.Id == id);
        }

        private void logToConsole(bool successful)
        {
            if (successful)
            {
                _logger.LogInformation($"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName} was called successfully." +
                                       $"\nAt {DateTime.Now}" +
                                       $"\nFrom {User.Identity.Name}");
            }
            else
            {
                _logger.LogError($"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName} failed." +
                                 $"\nAt {DateTime.Now}" +
                                 $"\nFrom {User.Identity.Name}");
            }
        }
    }
}
