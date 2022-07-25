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
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("[Action]")]
        public async Task<JsonResult> Index()
        {
              return _context.students != null ? 
                          Json(await _context.students.ToListAsync()) :
                          Json("Entity set 'ApplicationDbContext.students'  is null.");
        }

        [Route("[Action]")]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null || _context.students == null)
            {
                return Json("NotFound");
            }

            var student = await _context.students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return Json("NotFound");
            }

            return Json(student);
        }

        [Route("[Action]")]
        [HttpPost]
        public async Task<JsonResult> Create( Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return Json("Sucess");
            }
            return Json(student);
        }

        [Route("[Action]/{id}")]
        [HttpPost]
        public async Task<JsonResult> Edit(int id,Student student)
        {
            if (id != student.Id)
            {
                return Json("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return Json("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json("Sucess");
            }
            return Json(student);
        }

        [Route("[Action]/{id}")]
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            if (_context.students == null)
            {
                return Json("Entity set 'ApplicationDbContext.students'  is null.");
            }
            var student = await _context.students.FindAsync(id);
            if (student != null)
            {
                _context.students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return Json("Sucess");
        }

        private bool StudentExists(int id)
        {
          return (_context.students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
