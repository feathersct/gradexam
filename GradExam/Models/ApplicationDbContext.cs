using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradExam.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Concentration> Concentrations { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<QuesAnswer> QuesAnswer { get; set; }
        public DbSet<LearnOutcome> LearnOutcome { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<QuestionResponse> Responses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(bc => new { bc.StudentId, bc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(bc => bc.Student)
                .WithMany(b => b.StudentCourses)
                .HasForeignKey(bc => bc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(bc => bc.CourseId);

        }

    }
}
