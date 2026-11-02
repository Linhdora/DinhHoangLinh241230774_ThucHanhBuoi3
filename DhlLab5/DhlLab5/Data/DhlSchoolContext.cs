using DhlLab5.Models;
using Microsoft.EntityFrameworkCore;

namespace DhlLab5.Data;

public sealed class DhlSchoolContext(DbContextOptions<DhlSchoolContext> options) : DbContext(options)
{
    public DbSet<DhlCourse> Courses => Set<DhlCourse>();
    public DbSet<DhlLearner> Learners => Set<DhlLearner>();
    public DbSet<DhlEnrollment> Enrollments => Set<DhlEnrollment>();
    public DbSet<DhlMajor> Majors => Set<DhlMajor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DhlMajor>().ToTable("Major");
        modelBuilder.Entity<DhlCourse>().ToTable("Course");
        modelBuilder.Entity<DhlLearner>().ToTable("Learner");
        modelBuilder.Entity<DhlEnrollment>().ToTable("Enrollment");

        modelBuilder.Entity<DhlCourse>().HasKey(course => course.CourseID);
        modelBuilder.Entity<DhlLearner>().HasKey(learner => learner.LearnerID);
        modelBuilder.Entity<DhlEnrollment>().HasKey(enrollment => enrollment.EnrollmentID);
        modelBuilder.Entity<DhlMajor>().HasKey(major => major.MajorID);

        modelBuilder.Entity<DhlLearner>()
            .HasOne(learner => learner.Major)
            .WithMany(major => major.Learners)
            .HasForeignKey(learner => learner.MajorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DhlEnrollment>()
            .HasOne(enrollment => enrollment.Learner)
            .WithMany(learner => learner.Enrollments)
            .HasForeignKey(enrollment => enrollment.LearnerID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DhlEnrollment>()
            .HasOne(enrollment => enrollment.Course)
            .WithMany(course => course.Enrollments)
            .HasForeignKey(enrollment => enrollment.CourseID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DhlMajor>().HasData(
            new DhlMajor { MajorID = 1, MajorName = "IT" },
            new DhlMajor { MajorID = 2, MajorName = "Economics" },
            new DhlMajor { MajorID = 3, MajorName = "Mathematics" });

        modelBuilder.Entity<DhlLearner>().HasData(
            new DhlLearner { LearnerID = 1, FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = new DateTime(2005, 9, 1), MajorID = 1 },
            new DhlLearner { LearnerID = 2, FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = new DateTime(2002, 9, 1), MajorID = 2 });

        modelBuilder.Entity<DhlCourse>().HasData(
            new DhlCourse { CourseID = 1050, Title = "Chemistry", Credits = 3 },
            new DhlCourse { CourseID = 4022, Title = "Microeconomics", Credits = 3 },
            new DhlCourse { CourseID = 4041, Title = "Macroeconomics", Credits = 3 });

        modelBuilder.Entity<DhlEnrollment>().HasData(
            new DhlEnrollment { EnrollmentID = 1, LearnerID = 1, CourseID = 1050, Grade = 5.5f },
            new DhlEnrollment { EnrollmentID = 2, LearnerID = 1, CourseID = 4022, Grade = 7.5f },
            new DhlEnrollment { EnrollmentID = 3, LearnerID = 2, CourseID = 1050, Grade = 3.5f },
            new DhlEnrollment { EnrollmentID = 4, LearnerID = 2, CourseID = 4041, Grade = 7f });
    }
}
