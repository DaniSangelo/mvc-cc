
namespace Model.Registrations
{
    public class CourseDiscipline
    {
        public long? CourseId { get; set; }
        public Course Course { get; set; }
        public long? DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}
