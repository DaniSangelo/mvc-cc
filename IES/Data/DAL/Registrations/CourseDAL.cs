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

        public async Task<Course> SaveCourse(Course course)
        {
            if (course.CourseId == null)            
                _context.Courses.Add(course);            
            else            
                _context.Courses.Update(course);
            
            await _context.SaveChangesAsync();
            return course;
        }
    }
}
