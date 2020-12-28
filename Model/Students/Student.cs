using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

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

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDate { get; set; }

        public string PhotoMimeType { get; set; }
        public byte[] Photo { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
