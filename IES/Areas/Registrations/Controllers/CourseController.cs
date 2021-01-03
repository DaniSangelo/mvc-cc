using IES.Data;
using Model.Registrations;
using IES.Data.DAL.Registrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IES.Areas.Registrations.Controllers
{
    [Area("Registrations")]
    public class CourseController : Controller
    {
        private readonly IESContext _context;
        private readonly CourseDAL _courseDAL;
        private readonly DepartmentDAL _departmentDAL;

        public CourseController(IESContext context)
        {
            _context = context;
            _courseDAL = new CourseDAL(_context);
            _departmentDAL = new DepartmentDAL(_context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _courseDAL.GetCoursesOrderedByName().ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await GetViewCourseById(id);
        }

        #region CREATE

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentDAL.GetDepartmentsClassifiedByName().ToListAsync();
            departments.Insert(0, new Department() { DepartmentId = 0, Name = "Select the department" });
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? id, [Bind("CourseId, Name, DepartmentId")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _courseDAL.SaveCourse(course);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data");
            }
            return View(course);
        }

        #endregion

        #region EDIT

        public async Task<IActionResult> Edit(long? id)
        {
            var course = await _courseDAL.GetCourseById((long)id);
            ViewBag.Departments = new SelectList(_departmentDAL.GetDepartmentsClassifiedByName(), "DepartmentId", "Name", course.DepartmentId);
            return await GetViewCourseById((long)id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("CourseId, Name, DepartmentId")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _courseDAL.SaveCourse(course);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data");
            }
            ViewBag.Departments = new SelectList(_departmentDAL.GetDepartmentsClassifiedByName(), "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        #endregion

        #region GET
        private async Task<IActionResult> GetViewCourseById(long? id)
        {
            if (id == null)
                return NotFound();

            var course = await _courseDAL.GetCourseById((long)id);

            if (course.CourseId == null)
                return NotFound();

            return View(course);
        }

        #endregion
    }
}
