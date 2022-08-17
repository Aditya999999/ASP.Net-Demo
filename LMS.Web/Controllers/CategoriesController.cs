using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LMS.Web.Data;
using LMS.Web.Models;

namespace LMS.Web.Controllers
{
    /// <remarks>
    ///     In an ASP.NET Core REST API, there is no need to explicitly check if the model state is Valid. 
    ///     Since the controller class is decorated with the [ApiController] attribute, 
    ///     it takes care of checking if the model state is valid 
    ///     and automatically returns 400 response along the validation errors.
    ///     Example response:
    ///         {
    ///             "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    ///             "title": "One or more validation errors occurred.",
    ///             "status": 400,
    ///             "traceId": "|65b7c07c-4323622998dd3b3a.",
    ///             "errors": {
    ///                 "Email": [
    ///                     "The Email field is required."
    ///                 ],
    ///                 "FirstName": [
    ///                     "The field FirstName must be a string with a minimum length of 2 and a maximum length of 100."
    ///                 ]
    ///             }
    ///         }
    /// </remarks>


    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ApplicationDbContext context,
            ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        // public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        // {
        //      return await _context.Categories.ToListAsync();
        // }
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                if (categories == null)
                {
                    _logger.LogWarning("No Categories were found in the database");
                    return NotFound();
                }
                _logger.LogInformation("Extracted all the Categories from database");
                return Ok(categories);
            }
            catch
            {
                _logger.LogError("There was an attempt to retrieve information from the database");
                return BadRequest();
            }
        }


        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var category = await _context.Categories.FindAsync(id);
                //var category = await _context.Categories
                //                             .Include(c => c.Books)
                //                             .SingleOrDefaultAsync(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }



        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostCategory(Category category)
        {
            // NOTE: NO LONGER NEEDED.  Refer to the <remarks> in the top of this Controller Class
            //if (! ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                _context.Categories.Add(category);

                int countAffected = await _context.SaveChangesAsync();
                if (countAffected > 0)
                {
                    // Return the link to the newly inserted row 
                    var result = CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
                    return Ok(result);

                    // NOTE: Return the HTTP RESPONSE CODE 201 "Created"
                    // Uri url;
                    // System.Uri.TryCreate($"~/api/Categories/{category.CategoryId}", UriKind.Relative, out url); 
                    // return Created(url, result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("Post", exp.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}