using IES.Models.Infra;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Registrations;
using Model.Students;
using Model.Professor;

namespace IES.Data
{
    public class IESContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
       
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

            modelBuilder.Entity<CourseProfessor>()
                .HasKey(cp => new { cp.CourseId, cp.ProfessorId });

            modelBuilder.Entity<CourseProfessor>()
                .HasOne(c => c.Course)
                .WithMany(cp => cp.CourseProfessors)
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<CourseProfessor>()
                .HasOne(p => p.Professor)
                .WithMany(cp => cp.CourseProfessors)
                .HasForeignKey(p => p.ProfessorId);
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server(localdb)\\mssqllocaldb;Database=IESMVCCC; Trusted_Connection=True;MultipleActiveResultSet=true");
        }*/
    }
}
