using GQLDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GQLDomain.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            model.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrolls)
                .HasForeignKey(e => e.StudentId);

            model.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrolls)
                .HasForeignKey(e => e.CourseId);

            model.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId);
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Student> Students { get; set; }
        
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
