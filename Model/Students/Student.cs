using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Students
{
    public class Student
    {
        [Display(Name = "Student Id")]
        public long? StudentId { get; set; }
        
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("([0-9]{10})")]
        [Required]
        [Display(Name = "Student Register")]
        public string StudentRegister { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
        [Required]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}
