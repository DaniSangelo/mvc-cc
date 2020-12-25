using Model.Registrations;
using Model.Students;
using Microsoft.EntityFrameworkCore;

namespace IES.Data
{
    public class IESContext : DbContext
    {
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Student> Students { get; set; }
        public IESContext(DbContextOptions<IESContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseDiscipline>()
                .HasKey(cd => new { cd.CourseId, cd.DisciplineId });

            modelBuilder.Entity<CourseDiscipline>()
                .HasOne(c => c.Course)
                .WithMany(cd => cd.CoursesDisciplines)
                .HasForeignKey(c => c.CourseId);
            
            modelBuilder.Entity<CourseDiscipline>()
                .HasOne(d => d.Discipline)
                .WithMany(cd => cd.CoursesDisciplines)
                .HasForeignKey(d => d.DisciplineId);
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server(localdb)\\mssqllocaldb;Database=IESMVCCC; Trusted_Connection=True;MultipleActiveResultSet=true");
        }*/
    }
}
