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
    }
}
