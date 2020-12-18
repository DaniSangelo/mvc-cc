using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IES.Models
{
    public class Institution
    {
        [Display(Name = "Id")]
        public long? InstitutionId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
