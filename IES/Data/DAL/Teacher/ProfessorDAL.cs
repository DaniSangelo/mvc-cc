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

        public IQueryable<Professor> GetProfessorsClassifiedByName()
        {
            return _context.Professors.OrderBy(p => p.Name);
        }
    }
}
