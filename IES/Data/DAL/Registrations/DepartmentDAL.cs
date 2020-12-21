using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Registrations;

namespace IES.Data.DAL.Registrations
{
    public class DepartmentDAL
    {
        private IESContext _context;

        public DepartmentDAL(IESContext context)
        {
            _context = context;
        }       
    }
}
