using System.Collections.Generic;

namespace Model.Professor
{
    public class Professor
    {
        public long? ProfessorId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseProfessor> CourseProfessors { get; set; }
    }
}
