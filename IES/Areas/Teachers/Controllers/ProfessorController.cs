using IES.Data;
using IES.Data.DAL.Registrations;
using IES.Data.DAL.Teacher;
using Microsoft.AspNetCore.Mvc;
using Model.Registrations;
using Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IES.Areas.Teachers.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IES.Areas.Teachers.Controllers
{
    [Area("Teachers")]
    public class ProfessorController : Controller
    {
        private readonly IESContext _context;
        private readonly InstitutionDAL _institutionDAL;
        private readonly DepartmentDAL _departmentDAL;
        private readonly CourseDAL _courseDAL;
        private readonly ProfessorDAL _professorDAL;

        public ProfessorController(IESContext context)
        {
            _context = context;
            _institutionDAL = new InstitutionDAL(_context);
            _departmentDAL = new DepartmentDAL(_context);
            _courseDAL = new CourseDAL(_context);
            _professorDAL = new ProfessorDAL(_context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _professorDAL.GetProfessorsClassifiedByName().ToListAsync());
        }

        public async Task<IActionResult> Details(long id)
        {
            return (await GetViewProfessorById(id));
        }

        #region CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? id, [Bind("ProfessorId, Name")] Professor professor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _professorDAL.SaveProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data");
            }

            return View(professor);
        }
        #endregion

        #region EDIT
        public async Task<IActionResult> Edit(long? id)
        {
            return await GetViewProfessorById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ProfessorId, Name")] Professor professor)
        {
            if (id == null)
                return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    await _professorDAL.SaveProfessor(professor);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Wasn't possible to save data");
            }
            return View(professor);
        }
        #endregion

        #region DELETE
        public async Task<IActionResult> Delete(long? id)
        {
            return await GetViewProfessorById(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long? id, [Bind("ProfessorId, Name")] Professor professor)
        {
            if (id == null)
                return NotFound();
            await _professorDAL.RemoveProfessor(professor);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private async Task<IActionResult> GetViewProfessorById(long? id)
        {
            if (id == null)
                return NotFound();

            var professor = await _professorDAL.GetProfessorById((long)id);

            if (professor.ProfessorId == null)
                return NotFound();

            return View(professor);
        }

        public void PrepareViewBags(List<Institution> institutions, List<Department> departments, List<Course> courses, List<Professor> professors)
        {
            institutions.Insert(0, new Institution() { InstitutionId = 0, Name = "Select the institution" });
            ViewBag.Institutions = institutions;

            departments.Insert(0, new Department() { DepartmentId = 0, Name = "Select the department" });
            ViewBag.Departments = departments;

            courses.Insert(0, new Course() { CourseId = 0, Name = "Select the course" });
            ViewBag.Courses = courses;

            professors.Insert(0, new Professor() { ProfessorId = 0, Name = "Select the professor" });
            ViewBag.Professors = professors;

        }

        [HttpGet]
        public IActionResult AddProfessor()
        {
            PrepareViewBags(_institutionDAL.GetInstitutionsOrderedByName().ToList(), new List<Department>().ToList(), new List<Course>().ToList(), new List<Professor>().ToList());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProfessor([Bind("InstitutionId, DepartmentId, CourseId, ProfessorId")] AddProfessorViewModel model)
        {
            if (model.InstitutionId == 0 || model.DepartmentId == 0 || model.CourseId == 0 || model.ProfessorId == 0)
            {
                ModelState.AddModelError("", "All data must be informed");
            }
            else
            {
                _courseDAL.RegisterProfessor((long)model.CourseId, (long)model.ProfessorId);
                PrepareViewBags(_institutionDAL.GetInstitutionsOrderedByName().ToList(),
                    _departmentDAL.GetDepartmentsByInstitution((long)model.InstitutionId).ToList(),
                    _courseDAL.GetCoursesByDepartment((long)model.DepartmentId).ToList(),
                    _courseDAL.GetProfessorsOutOfCourse((long)model.CourseId).ToList());
            }
            return View(model);
        }

        public JsonResult GetDepartmentsByInstitution(long actionId)
        {
            var departments = _departmentDAL.GetDepartmentsByInstitution(actionId).ToList();
            return Json(new SelectList(departments, "DepartmentId", "Name"));
        }

        public JsonResult GetCoursesByDepartment(long actionId)
        {
            var courses = _courseDAL.GetCoursesByDepartment(actionId).ToList();
            return Json(new SelectList(courses, "CourseId", "Name"));
        }

        public JsonResult GetProfessorsOutOfCourse(long actionId)
        {
            var professors = _courseDAL.GetProfessorsOutOfCourse(actionId);
            return Json(new SelectList(professors, "ProfessorId", "Name"));
        }
    }
}
