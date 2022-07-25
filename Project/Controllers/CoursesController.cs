using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Project.Models;

namespace Project.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class  CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("[Action]")]
        public async Task<JsonResult> Index()
        {
              return _context.courses != null ? 
                          Json(await _context.courses.ToListAsync()) :
                          Json("Entity set 'ApplicationDbContext.courses'  is null.");
        }

        [Route("[Action]")]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return Json("Notfound");
            }

            var courses = await _context.courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courses == null)
            {
                return Json("Notfound");
            }

            return Json(courses);
        }



        [HttpPost]
        [Route("[Action]")]
        public async Task<JsonResult> Create(Courses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courses);
                await _context.SaveChangesAsync();
                return Json("Sucess");
            }
            return Json(courses);
        }



        [HttpPost]
        [Route("[Action]/{id}")]

        public async Task<JsonResult> Edit(int id, Courses courses)
        {
            if (id != courses.Id)
            {
                return Json("Notfound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.Id))
                    {
                        return Json("Notfound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json("Sucess");
            }
            return Json(courses);
        }


        [HttpPost, ActionName("Delete")]
        [Route("[Action]/{id}")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            if (_context.courses == null)
            {
                return Json("Entity set 'ApplicationDbContext.courses'  is null.");
            }
            var courses = await _context.courses.FindAsync(id);
            if (courses != null)
            {
                _context.courses.Remove(courses);
            }
            
            await _context.SaveChangesAsync();
            return Json("Sucess");
        }

        private bool CoursesExists(int id)
        {
          return (_context.courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
