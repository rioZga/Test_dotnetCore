using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) {}
        public DbSet<School> Schools { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => new { c.StudentId, c.ClasseId });
            modelBuilder.Entity<Course>()
                .HasOne(s => s.Student)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Classe)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.ClasseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
