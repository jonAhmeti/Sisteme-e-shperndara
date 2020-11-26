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
    public class ProvimetStudenteveController : ControllerBase
    {
        private readonly WebAPIContext _context;
        private ILogger<ProvimetStudenteve> _logger;

        public ProvimetStudenteveController(WebAPIContext context,ILogger<ProvimetStudenteve> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/ProvimetStudenteve
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<ProvimetStudenteve>>> GetProvimetStudenteves()
        {
            return await _context.ProvimetStudenteves.ToListAsync();
        }

        // GET: api/ProvimetStudenteve/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvimetStudenteve>> GetProvimetStudenteve(int id)
        {
            var provimetStudenteve = await _context.ProvimetStudenteves.FindAsync(id);

            if (provimetStudenteve == null)
            {
                return NotFound();
            }

            return provimetStudenteve;
        }

        // PUT: api/ProvimetStudenteve/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvimetStudenteve(int id, ProvimetStudenteve provimetStudenteve)
        {
            if (id != provimetStudenteve.Id)
            {
                return BadRequest();
            }

            _context.Entry(provimetStudenteve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvimetStudenteveExists(id))
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

        // POST: api/ProvimetStudenteve
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProvimetStudenteve>> PostProvimetStudenteve(ProvimetStudenteve provimetStudenteve)
        {
            if (await _context.ProvimetStudenteves.FirstOrDefaultAsync(provimiStudentit =>
                provimiStudentit.ProvimId == provimetStudenteve.ProvimId
                && provimiStudentit.StudentId == provimetStudenteve.StudentId) != null)
            {
                ModelState.AddModelError("Error:", "Provimi me 'ProvimiId' dhe 'StudentId' te dhene ekziston");
                logToConsole(false, ModelState.Values.Select(modelstate => modelstate.Errors[0].ErrorMessage).First());
                return BadRequest(ModelState);
            }
            _context.ProvimetStudenteves.Add(provimetStudenteve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvimetStudenteve", new { id = provimetStudenteve.Id }, provimetStudenteve);
        }

        // DELETE: api/ProvimetStudenteve/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProvimetStudenteve>> DeleteProvimetStudenteve(int id)
        {
            var provimetStudenteve = await _context.ProvimetStudenteves.FindAsync(id);
            if (provimetStudenteve == null)
            {
                return NotFound();
            }

            _context.ProvimetStudenteves.Remove(provimetStudenteve);
            await _context.SaveChangesAsync();

            return provimetStudenteve;
        }

        private bool ProvimetStudenteveExists(int id)
        {
            return _context.ProvimetStudenteves.Any(e => e.Id == id);
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
                        $"\nApi-Server message: {msg}" +
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
