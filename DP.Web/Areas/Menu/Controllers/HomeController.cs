using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DP.Web.Areas.Menu.ViewModels;
using DP.Web.Data;
using DP.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Web.Areas.Menu.Controllers
{
    [Area("Menu")]
    [Authorize(Roles ="Admin, Policemen")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Populate the data for the drop-down select list
            List<SelectListItem> complainers = new List<SelectListItem>();
            complainers.Add(new SelectListItem { Selected = true, Value = "", Text = "-- select a name --" });
            complainers.AddRange(new SelectList(_context.Complainers, "ComplainerId", "FirstName"));
            ViewData["ComplainerId"] = complainers.ToArray();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("ComplainerId")] ShowIncidentViewModel model)
        {
            // Retrieve the Menu Items for the selected category
            var items = _context.Incidents.Where(m => m.ComplainerId == model.ComplainerId);

            // Populate the data into the viewmodel object
            model.Incidents = items.ToList();

            // Populate the data for the drop-down select list
            ViewData["ComplainerId"] = new SelectList(_context.Complainers, "ComplainerId", "FirstName");

            // Display the View
            return View("Index", model);
        }
    }
}