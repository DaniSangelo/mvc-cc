using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Registrations
{
    public class Discipline
    {
        public long? DisciplineId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseDiscipline> CoursesDisciplines { get; set; }
    }
}
