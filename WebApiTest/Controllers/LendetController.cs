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
    public class LendetController : ControllerBase
    {
        private readonly WebAPIContext _context;
        private ILogger<Lendet> _logger;

        public LendetController(WebAPIContext context, ILogger<Lendet> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Lendet
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Lendet>>> GetLendets()
        {
            logToConsole(true);
            return await _context.Lendets.ToListAsync();
        }

        // GET: api/Lendet/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Lendet>> GetLendet(int id)
        {
            var lendet = await _context.Lendets.FindAsync(id);

            if (lendet == null)
            {
                logToConsole(false);
                return NotFound();
            }

            logToConsole(true);
            return lendet;
        }

        // PUT: api/Lendet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLendet(int id, Lendet lendet)
        {
            if (id != lendet.Id)
            {
                logToConsole(false);
                return BadRequest();
            }

            _context.Entry(lendet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LendetExists(id))
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

        // POST: api/Lendet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lendet>> PostLendet(Lendet lendet)
        {
            _context.Lendets.Add(lendet);
            await _context.SaveChangesAsync();
            logToConsole(true);

            return CreatedAtAction("GetLendet", new { id = lendet.Id }, lendet);
        }

        // DELETE: api/Lendet/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Lendet>> DeleteLendet(int id)
        {
            var lendet = await _context.Lendets.FindAsync(id);
            if (lendet == null)
            {
                logToConsole(false);
                return NotFound();
            }

            _context.Lendets.Remove(lendet);
            await _context.SaveChangesAsync();

            logToConsole(true);
            return lendet;
        }

        private bool LendetExists(int id)
        {
            return _context.Lendets.Any(e => e.Id == id);
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
