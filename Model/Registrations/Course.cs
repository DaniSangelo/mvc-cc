using System.Collections.Generic;
using Model.Professor;

namespace Model.Registrations
{
    public class Course
    {
        public long? CourseId { get; set; }
        public string Name { get; set; }
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<CourseDiscipline> CoursesDisciplines { get; set; }
        public virtual ICollection<CourseProfessor> CourseProfessors { get; set; }

    }
}
