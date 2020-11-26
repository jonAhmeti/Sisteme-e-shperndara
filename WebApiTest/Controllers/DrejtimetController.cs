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
    public class DrejtimetController : ControllerBase
    {
        private readonly WebAPIContext _context;
        private readonly ILogger<Drejtimet> _logger;

        public DrejtimetController(WebAPIContext context,ILogger<Drejtimet> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Drejtimet
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Drejtimet>>> GetDrejtimets()
        {
            return await _context.Drejtimets.ToListAsync();
        }

        // GET: api/Drejtimet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drejtimet>> GetDrejtimet(int id)
        {
            var drejtimet = await _context.Drejtimets.FindAsync(id);

            if (drejtimet == null)
            {
                return NotFound();
            }

            return drejtimet;
        }

        // PUT: api/Drejtimet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrejtimet(int id, Drejtimet drejtimet)
        {
            if (id != drejtimet.Id)
            {
                return BadRequest();
            }

            _context.Entry(drejtimet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrejtimetExists(id))
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

        // POST: api/Drejtimet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Drejtimet>> PostDrejtimet(Drejtimet drejtimet)
        {
            _context.Drejtimets.Add(drejtimet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrejtimet", new { id = drejtimet.Id }, drejtimet);
        }

        // DELETE: api/Drejtimet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drejtimet>> DeleteDrejtimet(int id)
        {
            var drejtimet = await _context.Drejtimets.FindAsync(id);
            if (drejtimet == null)
            {
                return NotFound();
            }

            _context.Drejtimets.Remove(drejtimet);
            await _context.SaveChangesAsync();

            return drejtimet;
        }

        private bool DrejtimetExists(int id)
        {
            return _context.Drejtimets.Any(e => e.Id == id);
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
