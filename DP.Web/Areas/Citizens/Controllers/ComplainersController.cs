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
using Newtonsoft.Json.Linq;

namespace DP.Web.Areas.Citizens.Controllers
{
    [Area("Citizens")]

    //Authorize only to AppAdmin, Citizen and Policemen to access this controller functionality.

    [Authorize(Roles ="AppAdmin, Citizen, Policemen")]
    public class ComplainersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComplainersController> _logger;

        public ComplainersController(ApplicationDbContext context,
            ILogger<ComplainersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Citizens/Complainers
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("-----Retrieved all the complainers from the database.");
            return View(await _context.Complainers.ToListAsync());
            
        }

        // GET: Citizens/Complainers/Details/5

        //Only AppAdmin and Policemen can see the details of complainers.

        [Authorize(Roles ="AppAdmin, Policemen")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complainer = await _context.Complainers
                .FirstOrDefaultAsync(m => m.ComplainerId == id);
            if (complainer == null)
            {
                return NotFound();
            }

            return View(complainer);
        }

        // GET: Citizens/Complainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Complainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        //The basic purpose of ValidateAntiForgeryToken attribute is to prevent cross-site request forgery attacks.
        //A cross-site request forgery is an attack in which a harmful script element, malicious command, or code is sent
        //from the browser of a trusted user.

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplainerId,AadharNumber,ImageUpload,FirstName,LastName,FathersName,NickName,Email,Gender,MaritalStatus,DateOfBirth,PhoneNumber,HouseNumber,Village,PostOffice,PinCode,District,State,Country")] Complainer complainer)
        {
            //Declaring isDuplicateFound variable to apply duplicate check by taking reference of Aadhar Number 
            bool isDuplicateFound
                = _context.Complainers.Any(c => c.AadharNumber == complainer.AadharNumber);

            if (isDuplicateFound)
            {
                ModelState.AddModelError("AadharNumber", "Duplicate! Another entry with same aadhar exists, you have to wait till another entry is not getting deleted.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(complainer);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof("Index");
                    return RedirectToAction("Index", "Incidents");
                   
                
                }
            }

                return View(complainer);
            
        }

        // GET: Citizens/Complainers/Edit/5

        //Only AppAdmin and Policemen have access to Editinig
        
        [Authorize(Roles ="AppAdmin, Policemen")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complainer = await _context.Complainers.FindAsync(id);
            if (complainer == null)
            {
                return NotFound();
            }
            return View(complainer);
        }

        // POST: Citizens/Complainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        //The basic purpose of ValidateAntiForgeryToken attribute is to prevent cross-site request forgery attacks.
        //A cross-site request forgery is an attack in which a harmful script element, malicious command, or code is sent
        //from the browser of a trusted user.

        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("ComplainerId,AadharNumber,ImageUpload,FirstName,LastName,FathersName,NickName,Email,Gender,MaritalStatus,DateOfBirth,PhoneNumber,HouseNumber,Village,PostOffice,PinCode,District,State,Country")] Complainer complainer)
        {
            if (id != complainer.ComplainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    try
                    {
                        _context.Update(complainer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ComplainerExists(complainer.ComplainerId))
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
            return View(complainer);
        }

        // GET: Citizens/Complainers/Delete/5

        //Authorized only AppAdmin and Policemen for Deleting access.

        [Authorize(Roles = "AppAdmin, Policemen")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complainer = await _context.Complainers
                .FirstOrDefaultAsync(m => m.ComplainerId == id);
            if (complainer == null)
            {
                return NotFound();
            }

            return View(complainer);
        }

        // POST: Citizens/Complainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complainer = await _context.Complainers.FindAsync(id);
            _context.Complainers.Remove(complainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplainerExists(int id)
        {
            return _context.Complainers.Any(e => e.ComplainerId == id);
        }
    }
}
