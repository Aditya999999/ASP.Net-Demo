using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DP.Web.Data;
using DP.Web.Models;

namespace DP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicemenDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PolicemenDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PolicemenDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicemenDetail>>> GetPolicemenDetails()
        {
            return await _context.PolicemenDetails.ToListAsync();
        }

        // GET: api/PolicemenDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PolicemenDetail>> GetPolicemenDetail(int id)
        {
            var policemenDetail = await _context.PolicemenDetails.FindAsync(id);

            if (policemenDetail == null)
            {
                return NotFound();
            }

            return policemenDetail;
        }

        // PUT: api/PolicemenDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicemenDetail(int id, PolicemenDetail policemenDetail)
        {
            if (id != policemenDetail.DetailId)
            {
                return BadRequest();
            }

            _context.Entry(policemenDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PolicemenDetailExists(id))
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

        // POST: api/PolicemenDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PolicemenDetail>> PostPolicemenDetail(PolicemenDetail policemenDetail)
        {
            _context.PolicemenDetails.Add(policemenDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPolicemenDetail", new { id = policemenDetail.DetailId }, policemenDetail);
        }

        // DELETE: api/PolicemenDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PolicemenDetail>> DeletePolicemenDetail(int id)
        {
            var policemenDetail = await _context.PolicemenDetails.FindAsync(id);
            if (policemenDetail == null)
            {
                return NotFound();
            }

            _context.PolicemenDetails.Remove(policemenDetail);
            await _context.SaveChangesAsync();

            return policemenDetail;
        }

        private bool PolicemenDetailExists(int id)
        {
            return _context.PolicemenDetails.Any(e => e.DetailId == id);
        }
    }
}
