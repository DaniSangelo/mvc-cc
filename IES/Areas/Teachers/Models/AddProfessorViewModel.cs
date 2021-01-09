using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Areas.Teachers.Models
{
    public class AddProfessorViewModel
    {
        [Display(Name = "Institution")]
        public long? InstitutionId { get; set; }
        [Display(Name = "Department")]
        public long? DepartmentId { get; set; }
        [Display(Name="Course")]
        public long? CourseId { get; set; }
        [Display(Name = "Professor")]
        public long? ProfessorId { get; set; }
    }
}
