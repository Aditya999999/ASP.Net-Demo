using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DP.Web.Data;
using DP.Web.Models;

namespace DP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComplainersController> _logger;

        public ComplainersController(ApplicationDbContext context, ILogger<ComplainersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Complainers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Complainer>>> GetComplainers()
        //{
        //    return await _context.Complainers.ToListAsync();
        //}
        public async Task<IActionResult> GetComplainers()
        {
            try
            {
                var complainers = await _context.Complainers.ToListAsync();
                if (complainers == null)
                {
                    _logger.LogWarning("No complainers were found in the database");
                    return NotFound();
                }
                _logger.LogInformation("Extracted all the complainers from the database");
                return Ok(complainers);
            }
            catch
            {
                _logger.LogError("There was an attempt to retrieve information from the database");
                return BadRequest();
            }
        }

        // GET: api/Complainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetComplainer(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var complainer = await _context.Complainers.FindAsync(id);
                if (complainer == null)
                {
                    return NotFound();
                }
                return Ok(complainer);

            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Complainers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplainer(int id, Complainer complainer)
        {
            if (id != complainer.ComplainerId)
            {
                return BadRequest();
            }

            _context.Entry(complainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplainerExists(id))
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

        // POST: api/Complainers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostComplainer(Complainer complainer)
        {
            try
            {
                _context.Complainers.Add(complainer);

                int countAffected = await _context.SaveChangesAsync();
                if (countAffected > 0)
                {
                    // Return the link to the newly inserted row
                    var result = CreatedAtAction("GetComplainer", new { id = complainer.ComplainerId }, complainer);
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("Post", exp.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Complainers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplainer(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var complainer = await _context.Complainers.FindAsync(id);
                if (complainer == null)
                {
                    return NotFound();
                }

                _context.Complainers.Remove(complainer);
                await _context.SaveChangesAsync();

                return Ok(complainer);
            }
            catch
            {
                return BadRequest();
            }
        }

        private bool ComplainerExists(int id)
        {
            return _context.Complainers.Any(e => e.ComplainerId == id);
        }
    }
}
