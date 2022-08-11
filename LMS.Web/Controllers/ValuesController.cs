using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return Ok(new string[] { "hello", "world" });
        //}

        //[HttpGet]
        //[ActionName("GetData")]
        //public IActionResult GetData()
        //{
        //    var obj = new { 
        //        Id = 10, 
        //        Name = "First Employee", 
        //        IsEnabled = true, 
        //        DOB = DateTime.Now 
        //    };

        //    return Ok(obj);
        //}

        [HttpGet]
        public IActionResult GetModelData()
        {
            LMS.Web.Models.Category category = new Models.Category()
            {
                CategoryId = 1,
                CategoryName = "First Category",
                Books = new LMS.Web.Models.Book[]
                {
                    new Models.Book {
                        BookId = 1,
                        BookTitle = "First Book",
                        CategoryId = 1,
                        Authors = new LMS.Web.Models.Author[] {
                            new Models.Author { AuthorId = 1, AuthorName = "First Author", BookId = 1}
                        }
                    }
                }
            };

            return Ok(category);
        }
    }
}

