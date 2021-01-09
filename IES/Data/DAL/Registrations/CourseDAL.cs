using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Registrations;
using Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Data.DAL.Registrations
{
    public class CourseDAL
    {
        private readonly IESContext _context;

        public CourseDAL(IESContext context)
        {
            _context = context;
        }

        #region GET
        public IQueryable<Course> GetCoursesOrderedByName()
        {
            return _context.Courses.Include(d => d.Department).OrderBy(c => c.Name);
        }

        public async Task<Course> GetCourseById(long id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(c => c.CourseId == id);
            _context.Departments.Where(d => course.DepartmentId == d.DepartmentId).Load();
            return course;
        }

        public IQueryable<Course> GetCoursesByDepartment(long departmentId)
        {
            var courses = _context.Courses.Where(c => c.DepartmentId == departmentId).OrderBy(d => d.Name);
            return courses;
        }

        public IQueryable<Professor> GetProfessorsOutOfCourse(long courseId)
        {
            var course = _context.Courses.Where(c => c.CourseId == courseId).Include(cp => cp.CoursesProfessors).First();
            var professorsOfCourse = course.CoursesProfessors.Select(cp => cp.ProfessorId).ToArray();
            var professorsOutOfCourse = _context.Professors.Where(p => !professorsOfCourse.Contains(p.ProfessorId));
            return professorsOutOfCourse;
        }
#endregion

        public async Task<Course> SaveCourse(Course course)
        {
            if (course.CourseId == null)            
                _context.Courses.Add(course);            
            else            
                _context.Courses.Update(course);
            
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public void RegisterProfessor(long courseId, long professorId)
        {
            var course = _context.Courses.Where(c => c.CourseId == courseId).Include(cp => cp.CoursesProfessors).First();
            var professor = _context.Professors.Find(professorId);
            course.CoursesProfessors.Add(new CourseProfessor() { Course = course, Professor = professor });
            _context.SaveChanges();
        }
    }
}
