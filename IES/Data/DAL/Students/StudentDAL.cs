using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Students;

namespace IES.Data.DAL.Students
{
    public class StudentDAL
    {
        private readonly IESContext _context;

        public StudentDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Student> GetStudentsClassifiedByName()
        {
            return _context.Students.OrderBy(s => s.Name);
        }

        public async Task<Student> GetStudentById(long id)
        {
            return await _context.Students.FindAsync(id); ;
        }

        public async Task<Student> SaveStudent(Student student)
        {
            if (student.StudentId == null)
            {
                _context.Students.Add(student);
            }
            else
            {
                _context.Students.Update(student);
            }
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> DeleteStudent(long id)
        {
            var student = await GetStudentById(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
