using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Areas.Demos.Controllers
{
    [Area("Demos")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
