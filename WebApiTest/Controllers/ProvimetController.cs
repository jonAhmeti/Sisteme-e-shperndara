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
    [Route("Api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProvimetController : ControllerBase
    {
        private readonly WebAPIContext _context;
        private ILogger<Provimet> _logger;

        public ProvimetController(WebAPIContext context, ILogger<Provimet> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Provimet
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Provimet>>> GetProvimets()
        {
            logToConsole(true);
            return await _context.Provimets.ToListAsync();
        }

        // GET: api/Provimet/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Provimet>> GetProvimet(int id)
        {
            var provimet = await _context.Provimets.FindAsync(id);

            if (provimet == null)
            {
                logToConsole(false);
                return NotFound();
            }

            logToConsole(true);
            return provimet;
        }

        // PUT: api/Provimet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProvimet(int id, Provimet provimet)
        {
            if (id != provimet.Id)
            {
                logToConsole(false);
                return BadRequest();
            }

            _context.Entry(provimet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvimetExists(id))
                {
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

        // POST: api/Provimet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Provimet>> PostProvimet(Provimet provimet)
        {
            if ( await _context.Provimets.FirstOrDefaultAsync( provimi => 
                provimi.LendaId == provimet.LendaId && provimi.Data == provimet.Data) != null)
            {
                ModelState.AddModelError("Error:", "Provimi me Lenden dhe Daten e dhene ekziston");
                logToConsole(false,ModelState.Values.Select(modelstate => modelstate.Errors[0].ErrorMessage).First());
                return BadRequest(ModelState);
            }
            _context.Provimets.Add(provimet);
            await _context.SaveChangesAsync();

            logToConsole(true);
            return CreatedAtAction("GetProvimet", new { id = provimet.Id }, provimet);
        }

        // DELETE: api/Provimet/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Provimet>> DeleteProvimet(int id)
        {
            var provimet = await _context.Provimets.FindAsync(id);
            if (provimet == null)
            {
                logToConsole(false);
                return NotFound();
            }

            _context.Provimets.Remove(provimet);
            await _context.SaveChangesAsync();

            logToConsole(true);
            return provimet;
        }

        private bool ProvimetExists(int id)
        {
            return _context.Provimets.Any(e => e.Id == id);
        }

        private void logToConsole(bool successful, string msg = "")
        {
            if (successful)
            {
                _logger.LogInformation($"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName} was called successfully." +
                                       $"\nAt {DateTime.Now}" +
                                       $"\nFrom {User.Identity.Name}");
            }
            else
            {
                if (!String.IsNullOrEmpty(msg))
                    _logger.LogError(
                        $"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName} failed." +
                        $"\nApi-Server message: {msg}"+
                        $"\nAt {DateTime.Now}" +
                        $"\nFrom {User.Identity.Name}");
                else
                    _logger.LogError(
                        $"{ControllerContext.ActionDescriptor.ControllerName}/{ControllerContext.ActionDescriptor.ActionName} failed." +
                        $"\nAt {DateTime.Now}" +
                        $"\nFrom {User.Identity.Name}");
            }
        }
    }
}
