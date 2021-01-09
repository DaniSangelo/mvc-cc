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

        public void PrepareViewBags(List<Institution> institutions, List<Department> departments, List<Course> courses, List<Professor> professors)
        {
            institutions.Insert(0, new Institution() { InstitutionId = 0, Name = "Select the institution" });
            ViewBag.Institutions = institutions;

            departments.Insert(0, new Department() { DepartmentId = 0, Name = "Select the department" });
            ViewBag.Institutions = departments;

            courses.Insert(0, new Course() { CourseId = 0, Name = "Select the course" });
            ViewBag.Institutions = courses;

            professors.Insert(0, new Professor() { ProfessorId = 0, Name = "Select the professor" });
            ViewBag.Institutions = professors;

        }

        [HttpGet]
        public IActionResult AddPrfessor()
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

        public async Task<IActionResult> Index()
        {
            var professors = await _professorDAL.GetProfessorsClassifiedByName().ToListAsync();
            return View(professors);
        }
    }
}
