using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Teacher;

namespace Model.Registrations
{
    public class Course
    {
        public long? CourseId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Department")]
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<CourseDiscipline> CoursesDisciplines { get; set; }
        public virtual ICollection<CourseProfessor> CoursesProfessors { get; set; }
    }
}
