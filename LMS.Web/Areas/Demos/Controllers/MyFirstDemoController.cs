using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Areas.Demos.Controllers
{
    [Area("Demos")]
    public class MyFirstDemoController : Controller
    {
        // url / {area}/ {controller}/{action}
        public IActionResult Index()
        {
            return Ok("Hello World!!!");
        }
        public IActionResult Index2()
        { 
            return View();
        }
    }
}
