using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DP.Web.Data;
using DP.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DP.Web.Areas.Policemens.Controllers
{
    [Area("Policemens")]
    [Authorize(Roles = "AppAdmin, Policemen")]
    public class ComplaintAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Policemens/ComplaintAssignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComplaintAssignments.Include(c => c.Complainer).Include(c => c.PolicemenDetail);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Policemens/ComplaintAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintAssignment = await _context.ComplaintAssignments
                .Include(c => c.Complainer)
                .Include(c => c.PolicemenDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintAssignment == null)
            {
                return NotFound();
            }

            return View(complaintAssignment);
        }

        // GET: Policemens/ComplaintAssignments/Create
        [Authorize(Roles = "AppAdmin")]
        public IActionResult Create()
        {
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber");
            ViewData["PolicemenId"] = new SelectList(_context.PolicemenDetails, "DetailId", "AadharNumber");
            return View();
        }

        // POST: Policemens/ComplaintAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComplainerId,PolicemenId,AssignedDate,IsAssigned,IsResolved,ComplaintResolvedDate")] ComplaintAssignment complaintAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber", complaintAssignment.ComplainerId);
            ViewData["PolicemenId"] = new SelectList(_context.PolicemenDetails, "DetailId", "AadharNumber", complaintAssignment.PolicemenId);
            return View(complaintAssignment);
        }

        // GET: Policemens/ComplaintAssignments/Edit/5
        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintAssignment = await _context.ComplaintAssignments.FindAsync(id);
            if (complaintAssignment == null)
            {
                return NotFound();
            }
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber", complaintAssignment.ComplainerId);
            ViewData["PolicemenId"] = new SelectList(_context.PolicemenDetails, "DetailId", "AadharNumber", complaintAssignment.PolicemenId);
            return View(complaintAssignment);
        }

        // POST: Policemens/ComplaintAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,ComplainerId,PolicemenId,AssignedDate,IsAssigned,IsResolved,ComplaintResolvedDate")] ComplaintAssignment complaintAssignment)
        {
            if (id != complaintAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintAssignmentExists(complaintAssignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber", complaintAssignment.ComplainerId);
            ViewData["PolicemenId"] = new SelectList(_context.PolicemenDetails, "DetailId", "AadharNumber", complaintAssignment.PolicemenId);
            return View(complaintAssignment);
        }

        // GET: Policemens/ComplaintAssignments/Delete/5
        [Authorize(Roles = "AppAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintAssignment = await _context.ComplaintAssignments
                .Include(c => c.Complainer)
                .Include(c => c.PolicemenDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complaintAssignment == null)
            {
                return NotFound();
            }

            return View(complaintAssignment);
        }

        // POST: Policemens/ComplaintAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complaintAssignment = await _context.ComplaintAssignments.FindAsync(id);
            _context.ComplaintAssignments.Remove(complaintAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintAssignmentExists(int id)
        {
            return _context.ComplaintAssignments.Any(e => e.Id == id);
        }
    }
}
