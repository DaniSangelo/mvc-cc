using System;
using System.Collections.Generic;
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
    }
}
