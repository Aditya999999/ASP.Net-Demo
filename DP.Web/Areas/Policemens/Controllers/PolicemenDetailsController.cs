using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DP.Web.Data;
using DP.Web.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace DP.Web.Areas.Policemens.Controllers
{
    [Area("Policemens")]
    [Authorize(Roles ="AppAdmin, Policemen")]
    public class PolicemenDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PolicemenDetailsController> _logger;

        public PolicemenDetailsController(ApplicationDbContext context,
            ILogger<PolicemenDetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Policemens/PolicemenDetails
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("-----Retrieved all the policemen details from the database.");
            var applicationDbContext = _context.PolicemenDetails.Include(p => p.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Policemens/PolicemenDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policemenDetail = await _context.PolicemenDetails
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (policemenDetail == null)
            {
                return NotFound();
            }

            return View(policemenDetail);
        }

        // GET: Policemens/PolicemenDetails/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Policemens/PolicemenDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetailId,Name,FathersName,AadharNumber,DateOfBirth,Gender,Rank,DateOfJoining,ImageUpload,DepartmentId,PlaceOfPosting")] PolicemenDetail policemenDetail)
        {
            //Impleting Duplicate check for creating user with same Aadhar number

            bool isDuplicateFound
                = _context.PolicemenDetails.Any(p => p.AadharNumber == policemenDetail.AadharNumber);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("AadharNumber", "Duplicate! Another policemen entry with same aadhar exists..");
            }
            else
            {
                if (ModelState.IsValid)

                {
                    _context.Add(policemenDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", policemenDetail.DepartmentId);
            return View(policemenDetail);
        }

        // GET: Policemens/PolicemenDetails/Edit/5
        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policemenDetail = await _context.PolicemenDetails.FindAsync(id);
            if (policemenDetail == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", policemenDetail.DepartmentId);
            return View(policemenDetail);
        }

        // POST: Policemens/PolicemenDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetailId,Name,FathersName,AadharNumber,DateOfBirth,Gender,Rank,DateOfJoining,ImageUpload,DepartmentId,PlaceOfPosting")] PolicemenDetail policemenDetail)
        {
            if (id != policemenDetail.DetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isFound
                    = _context.PolicemenDetails.Any(p => p.AadharNumber == policemenDetail.AadharNumber);
                if (isFound)
                {
                    ModelState.AddModelError("AadharNumber", "A duplicate entry with same aadhar number exists.");
                }
                else
                {
                    try
                    {
                        _context.Update(policemenDetail);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PolicemenDetailExists(policemenDetail.DetailId))
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
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", policemenDetail.DepartmentId);
            return View(policemenDetail);
        }

        // GET: Policemens/PolicemenDetails/Delete/5
        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policemenDetail = await _context.PolicemenDetails
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.DetailId == id);
            if (policemenDetail == null)
            {
                return NotFound();
            }

            return View(policemenDetail);
        }

        // POST: Policemens/PolicemenDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policemenDetail = await _context.PolicemenDetails.FindAsync(id);
            _context.PolicemenDetails.Remove(policemenDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicemenDetailExists(int id)
        {
            return _context.PolicemenDetails.Any(e => e.DetailId == id);
        }
    }
}
