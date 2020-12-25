using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Students
{
    public class Student
    {
        public long? StudentId { get; set; }
        
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("([0-9]{10})")]
        [Required]
        public string StudentRegister { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy")]
        [Required]
        public DateTime? BirthDate { get; set; }
    }
}
