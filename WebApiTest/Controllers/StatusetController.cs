using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [AllowAnonymous]
    [Route("Api/[controller]")]
    [ApiController]
    public class StatusetController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public StatusetController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Statuset
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Statuset>>> GetStatusets()
        {
            return await _context.Statusets.ToListAsync();
        }

        // GET: api/Statuset/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Statuset>> GetStatuset(int id)
        {
            var statuset = await _context.Statusets.FindAsync(id);

            if (statuset == null)
            {
                return NotFound();
            }

            return statuset;
        }

        /*
        // PUT: api/Statuset/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatuset(int id, Statuset statuset)
        {
            if (id != statuset.Id)
            {
                return BadRequest();
            }

            _context.Entry(statuset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusetExists(id))
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

        // POST: api/Statuset
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Statuset>> PostStatuset(Statuset statuset)
        {
            _context.Statusets.Add(statuset);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatuset", new { id = statuset.Id }, statuset);
        }

        // DELETE: api/Statuset/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Statuset>> DeleteStatuset(int id)
        {
            var statuset = await _context.Statusets.FindAsync(id);
            if (statuset == null)
            {
                return NotFound();
            }

            _context.Statusets.Remove(statuset);
            await _context.SaveChangesAsync();

            return statuset;
        }
        */

        private bool StatusetExists(int id)
        {
            return _context.Statusets.Any(e => e.Id == id);
        }
    }
}
