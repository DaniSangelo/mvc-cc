using IES.Data;
using IES.Data.DAL.Registrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Areas.Registrations.Controllers
{
    [Area("Registrations")]
    public class CourseController : Controller
    {
        private readonly IESContext _context;
        private readonly CourseDAL _courseDAL;

        public CourseController(IESContext context)
        {
            _context = context;
            _courseDAL = new CourseDAL(_context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _courseDAL.GetCoursesOrderedByName().ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetViewCourseById(id);
        }

        private async Task<IActionResult> GetViewCourseById(long? id)
        {
            if (id == null)
                return NotFound();

            var course = await _courseDAL.GetCourseById((long)id);

            if (course.CourseId == null)
                return NotFound();

            return View(course);
        }
    }
}
