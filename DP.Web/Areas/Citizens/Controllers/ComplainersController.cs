﻿using System;
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
using DP.Web.Areas.Citizens.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace DP.Web.Areas.Citizens.Controllers
{
    [Area("Citizens")]

    //Authorize only to AppAdmin, Citizen and Policemen to access this controller functionality.

    [Authorize(Roles ="AppAdmin, Citizen, Policemen")]
    public class ComplainersController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComplainersController> _logger;
        private readonly IHostingEnvironment hostingEnvironment;
        public ComplainersController(ApplicationDbContext context,
            ILogger<ComplainersController> logger,
            IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Citizens/Complainers
        [Authorize(Roles ="AppAdmin, Policemen")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("-----Retrieved all the complainers from the database.");
            return View(await _context.Complainers.ToListAsync());
            
        }

        // GET: Citizens/Complainers/Details/5

        //Only AppAdmin and Policemen can see the details of complainers.

        [Authorize(Roles ="AppAdmin, Policemen, Citizen")]
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
        public async Task<IActionResult> Create(ComplainerViewModel viewModel,
            ComplainerCreateViewModel model)
        {
            //1. Authentication

            //2. Authorization

            //Declaring isDuplicateFound variable to apply duplicate check by taking reference of Aadhar Number 
            bool isDuplicateFound
                = _context.Complainers.Any(c => c.AadharNumber == model.AadharNumber);

            if (isDuplicateFound)
            {
                ModelState.AddModelError("AadharNumber", "Duplicate! Another entry with same aadhar exists, you have to wait till another entry is not getting deleted.");
            }
            
            else
            {
                //3. Validation (Perform server side validation)
                if (ModelState.IsValid)
                {
                    //Check if the DoB is greater than 18 years
                    if (System.DateTime.Now.Year - 18 < model.DateOfBirth.Year)
                    {
                        ModelState.AddModelError("DateOfBirth", "Date of Birth should be greater than 18 years! ");
                    }
                }

                //4. Activity / action (perform server side activity)
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (model.ImageUpload != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        model.ImageUpload.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    Complainer newComplainer = new Complainer
                    {
                        AadharNumber = model.AadharNumber,
                        ImageUpload = uniqueFileName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        FathersName = model.FathersName,
                        Email = model.Email,
                        NickName = model.NickName,
                        Gender = model.Gender,
                        MaritalStatus = model.MaritalStatus,
                        DateOfBirth = model.DateOfBirth,
                        PhoneNumber = model.PhoneNumber,
                        HouseNumber = model.HouseNumber,
                        Village = model.Village,
                        PostOffice = model.PostOffice,
                        PinCode = model.PinCode,
                        District = model.District,
                        State = model.State,
                        Country = model.Country
                    };
                    _context.Add(newComplainer);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof("Index");
                    //return RedirectToAction("Create", "Incidents");
                    return RedirectToAction("Details", new { id = newComplainer.ComplainerId });
                
                }
            }
                //5. Audit Logging
                //return View(complainer);
                return View(model);
            
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
                //Exception-handling for Editing the details 
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
