using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Registrations
{
    public class Course
    {
        public long? CourseId { get; set; }
        public string Name { get; set; }
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }
        public virtual ICollection<CourseDiscipline> CoursesDisciplines { get; set; }


    }
}
