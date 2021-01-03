using System;
using System.Collections.Generic;
using System.Text;
using Model.Registrations;

namespace Model.Teacher
{
    public class CourseProfessor
    {
        public long? CourseId { get; set; }
        public Course Course { get; set; }
        public long? ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}
