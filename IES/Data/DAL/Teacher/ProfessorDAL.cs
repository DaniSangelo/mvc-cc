using IES.Data.DAL.Registrations;
using IES.Data.DAL.Teacher;
using Model.Teacher;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Data.DAL.Teacher
{
    public class ProfessorDAL
    {
        private readonly IESContext _context;

        public ProfessorDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Professor> GetProfessorOrderedByName()
        {
            return _context.Professors.OrderBy(p => p.Name);
        }

        public async Task<Professor> GetProfessorById(long? id)
        {
            return (await _context.Professors.FindAsync(id));
        }

        public async Task<Professor> SaveProfessor(Professor professor)
        {
            if (professor.ProfessorId == null)
            {
                _context.Professors.Add(professor);
            }
            else
            {
                _context.Professors.Update(professor);
            }
            await _context.SaveChangesAsync();
            return professor;
        }

        public async Task<Professor> DeleteProfessor(Professor professor)
        {
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
            return professor;
        }
    }
}
