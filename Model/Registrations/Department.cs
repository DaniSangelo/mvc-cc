using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Registrations
{
    public class Department
    {
        [Display(Name = "Id")]
        public long? DepartmentId { get; set; }

        [Required(ErrorMessage = "Department {0} must be informed")]
        public string Name { get; set; }

        [Display(Name="Institution")]
        [Required(ErrorMessage = "Institution must be informed")]
        public long? InstitutionId { get; set; }
        public Institution Institution { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
