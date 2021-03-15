using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Teacher
{
    public class Professor
    {
		//Sourcetree
        public long? ProfessorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseProfessor> CoursesProfessors { get; set; }
    }
}
