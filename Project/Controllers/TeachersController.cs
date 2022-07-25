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
    public class TeachersController : Controller
    {   
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("[Action]")]
        public async Task<JsonResult> Index()
        {
              return _context.teachers != null ? 
                          Json(await _context.teachers.ToListAsync()) :
                          Json("Entity set 'ApplicationDbContext.teachers'  is null.");
        }

        [Route("[Action]")]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null || _context.teachers == null)
            {
                return Json("NotFound");
            }

            var teachers = await _context.teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teachers == null)
            {
                return Json("NotFound");
            }

            return Json(teachers);
        }


        [HttpPost]
        [Route("[Action]")]
        public async Task<JsonResult> Create(Teachers teachers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachers);
                await _context.SaveChangesAsync();
                return Json("Sucess");
            }
            return Json(teachers);
        }

        [HttpPost]
        [Route("[Action]/{id}")]
        public async Task<JsonResult> Edit(int id,Teachers teachers)
        {
            if (id != teachers.Id)
            {
                return Json("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachersExists(teachers.Id))
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
            return Json(teachers);
        }


        [HttpPost, ActionName("Delete")]
        [Route("[Action]/{id}")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            if (_context.teachers == null)
            {
                return Json("Entity set 'ApplicationDbContext.teachers'  is null.");
            }
            var teachers = await _context.teachers.FindAsync(id);
            if (teachers != null)
            {
                _context.teachers.Remove(teachers);
            }
            
            await _context.SaveChangesAsync();
            return Json("Sucess");
        }

        private bool TeachersExists(int id)
        {
          return (_context.teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
