using DP.Web.Areas.Menu.ViewModels;
using DP.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DP.Web.Areas.Menu.Controllers
{
    [Area("Menu")]
    [Authorize(Roles = "Admin, Policemen")]
    public class ValueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Populate the data for the drop-down select list
            List<SelectListItem> departments = new List<SelectListItem>();
            departments.Add(new SelectListItem { Selected = true, Value = "", Text = "-- select a department --" });
            departments.AddRange(new SelectList(_context.Departments, "DepartmentId", "DepartmentName"));
            ViewData["DepartmentId"] = departments.ToArray();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("DepartmentId")] ShowPolicemenViewModel model)
        {
            // Retrieve the policemen for the selected department
            var items = _context.PolicemenDetails.Where(m => m.DepartmentId == model.DepartmentId);

            // Populate the data into the viewmodel object
            model.PolicemenDetails = items.ToList();

            // Populate the data for the drop-down select list
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");

            // Display the View
            return View("Index", model);
        }
    }
}
