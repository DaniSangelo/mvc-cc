using IES.Data.DAL.Registrations;
using IES.Data.DAL.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Data.DAL.Teacher
{
    public class ProfessorDAL
    {
        private readonly IESContext _context;
        private readonly InstitutionDAL _institutionDAL;
        private readonly DepartmentDAL _departmentDAL;
        private readonly CourseDAL _courseDAL;
        private readonly ProfessorDAL _professorDAL;

        public ProfessorDAL(IESContext context)
        {
            _context = context;
            _institutionDAL = new InstitutionDAL(_context);
            _departmentDAL = new DepartmentDAL(_context);
            _courseDAL = new CourseDAL(_context);
            _professorDAL = new ProfessorDAL(_context);
        }
    }
}
