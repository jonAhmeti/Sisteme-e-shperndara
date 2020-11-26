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
using WebApiTest.Security;

namespace WebApiTest.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class StudentetController : ControllerBase
    {
        private readonly WebAPIContext _context;
        private ILogger<Studentet> _logger;

        public StudentetController(WebAPIContext context, ILogger<Studentet> logger)
        {
            _context = context;
            _logger = logger;
        }

        

        // GET: api/Studentet
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Studentet>>> GetStudentets()
        {
            logToConsole(true);
            return await _context.Studentets.OrderBy(key => key.Id).ToListAsync();
        }

        // GET: api/Studentet/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Studentet>> GetStudentet(int id)
        {
            var studentet = await _context.Studentets.FindAsync(id);

            if (studentet == null)
            {
                logToConsole(false);
                return NotFound();
            }

            logToConsole(true);
            return studentet;
        }

        // PUT: api/Studentet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentet(int id, Studentet studentet)
        {
            if (id != studentet.Id)
            {
                logToConsole(false); 
                ModelState.AddModelError("Id", "The object you sent does not have the same Id as the one in the URI.");
                return BadRequest(ModelState);
            }

            studentet.UpdateDate = DateTime.Now;
            _context.Entry(studentet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentetExists(id))
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

        // POST: api/Studentet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Studentet>> PostStudentet(Studentet studentet)
        {
            //-	Ekspozimi I WebShebim / WebAPI qe regjistron nje stundet dhe
            //nese studenti me indeks te dhene ekziston, atehere te kthehet
            //mesazhi qe student me ID-n e dhene, egziston.
            if (StudentetExists(studentet.Id))
            {
                logToConsole(false);
                ModelState.AddModelError("Id", "Studenti me ID-në e dhënë ekziston!");
                return BadRequest(ModelState);
            }

            _context.Studentets.Add(studentet);
            await _context.SaveChangesAsync();

            logToConsole(true);
            return CreatedAtAction("GetStudentet", new { id = studentet.Id }, studentet);
        }

        // DELETE: api/Studentet/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Studentet>> DeleteStudentet(int id)
        {
            var studentet = await _context.Studentets.FindAsync(id);
            if (studentet == null)
            {
                logToConsole(false);
                return NotFound();
            }

            try
            {
                _context.Studentets.Remove(studentet);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logToConsole(false,e.InnerException.Message);
                return BadRequest();
            }

            logToConsole(true);
            return studentet;
        }

        private bool StudentetExists(int id)
        {
            return _context.Studentets.Any(e => e.Id == id);
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
