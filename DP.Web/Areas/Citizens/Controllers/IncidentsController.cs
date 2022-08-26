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

namespace DP.Web.Areas.Citizens.Controllers
{
    [Area("Citizens")]

    //Authorize only to AppAdmin , Citizen and Policemen to access the Incident Controller.

    [Authorize(Roles ="AppAdmin, Citizen, Policemen")]
    public class IncidentsController : Controller
    {
        //readonly is a constant defined at runtime.

        private readonly ApplicationDbContext _context;

        public IncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Citizens/Incidents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Incidents.Include(i => i.Complainer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Citizens/Incidents/Details/5

        //Authorize only AppAdmin and Policemen to access Details of Incidents Occured.

        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .Include(i => i.Complainer)        //Including Complainer Explicitly because ComplainerId is Foreign Key from another table.  
                .FirstOrDefaultAsync(m => m.IncidentId == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // GET: Citizens/Incidents/Create
        public IActionResult Create()
        {
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber"); //Populate the aadhar number from Complainers Table in List.
            return View();
        }

        // POST: Citizens/Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        //The basic purpose of ValidateAntiForgeryToken attribute is to prevent cross-site request forgery attacks.
        //A cross-site request forgery is an attack in which a harmful script element, malicious command, or code is sent
        //from the browser of a trusted user.

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidentId,IncidentDetail,FileUpload,NumberOfPeopleDied,NumberOfPeopleInjured,IsVideoFootageAvailable,ComplainerId")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incident);
                await _context.SaveChangesAsync();
               
                //return RedirectToAction(nameof(Index));
                
                return View("Confirmation");
            }
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber", incident.ComplainerId);
            return View(incident);
            
        }

        // GET: Citizens/Incidents/Edit/5

        //Authorizing AppAdmin and Policemen to access the Edit Section.

        [Authorize(Roles ="AppAdmin, Policemen")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber", incident.ComplainerId);
            return View(incident);
        }

        // POST: Citizens/Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        //The basic purpose of ValidateAntiForgeryToken attribute is to prevent cross-site request forgery attacks.
        //A cross-site request forgery is an attack in which a harmful script element, malicious command, or code is sent
        //from the browser of a trusted user.

        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("IncidentId,IncidentDetail,FileUpload,NumberOfPeopleDied,NumberOfPeopleInjured,IsVideoFootageAvailable,ComplainerId")] Incident incident)
        {
            if (id != incident.IncidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.IncidentId))
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
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "AadharNumber", incident.ComplainerId);
            return View(incident);
        }

        // GET: Citizens/Incidents/Delete/5
        //Authorize AppAdmin and Policemen to access delete

        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _context.Incidents
                .Include(i => i.Complainer)
                .FirstOrDefaultAsync(m => m.IncidentId == id);
            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        // POST: Citizens/Incidents/Delete/5
        [HttpPost, ActionName("Delete")]

        //The basic purpose of ValidateAntiForgeryToken attribute is to prevent cross-site request forgery attacks.
        //A cross-site request forgery is an attack in which a harmful script element, malicious command, or code is sent
        //from the browser of a trusted user.

        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentExists(int id)
        {
            return _context.Incidents.Any(e => e.IncidentId == id);
        }
    }
}
