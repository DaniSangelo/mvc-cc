using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Areas.Teachers.Models
{
    public class AddProfessorViewModel
    {
        public long? InstitutionId { get; set; }
        public long? DepartmentId { get; set; }
        public long? CourseId { get; set; }
        public long? ProfessorId { get; set; }
    }
}
