using System;
using Microsoft.EntityFrameworkCore;

namespace Practice_API_2.Classes
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }

        public StudentContext(DbContextOptions<StudentContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseMySQL($"server=localhost;port=3444;userid=root;password=khoa444;database=Student");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData
            (
                new Student() { StudentId = 1, StudentName = "Tom", Gender = "Male",DateOfBirth = DateTime.Parse("1986-01-01") ,isDeleted = false},
                new Student() { StudentId = 2, StudentName = "Jerry",Gender = "Male",DateOfBirth = DateTime.Parse("1986-05-01"),isDeleted = false},
                new Student() { StudentId = 3, StudentName = "Max",Gender = "Female",DateOfBirth = DateTime.Parse("1986-07-01"),isDeleted = false}
            );
            modelBuilder.Entity<Subject>().HasData
            (
                new Subject() {SubjectId = 1, SubjectName = "Math",Teacher = "Ming",isDeleted = false},
                new Subject() {SubjectId = 2, SubjectName = "English",Teacher = "Isabell",isDeleted = false},
                new Subject() {SubjectId = 3, SubjectName = "Chemistry",Teacher = "Peter",isDeleted = false}
            );
            modelBuilder.Entity<StudentClass>().HasData
            (
                new StudentClass() {StudentId = 1,SubjectId = 1, DateTimeStart = DateTime.Parse("2022-01-01") , DateTimeEnd = DateTime.Parse("2022-07-01"), isDeleted = false},
                new StudentClass() {StudentId = 3,SubjectId = 2, DateTimeStart = DateTime.Parse("2022-02-01") , DateTimeEnd = DateTime.Parse("2022-08-01"), isDeleted = false},
                new StudentClass() {StudentId = 3,SubjectId = 3, DateTimeStart = DateTime.Parse("2021-01-07") , DateTimeEnd = DateTime.Parse("2021-07-07"), isDeleted = false}
                
            );
            
            modelBuilder.Entity<StudentClass>()
                .HasKey(t => new { t.StudentId, t.SubjectId });

            modelBuilder.Entity<StudentClass>()
                .HasOne(pt => pt.Student)
                .WithMany(p => p.StudentClasses)
                .HasForeignKey(pt => pt.StudentId);

            modelBuilder.Entity<StudentClass>()
                .HasOne(pt => pt.Subject)
                .WithMany(t => t.StudentClasses)
                .HasForeignKey(pt => pt.SubjectId);
            
            

            // take query with .isdeleted = false
            modelBuilder.Entity<Student>().HasQueryFilter(stu => !stu.isDeleted);
            modelBuilder.Entity<Subject>().HasQueryFilter(sub => !sub.isDeleted);
            modelBuilder.Entity<StudentClass>().HasQueryFilter(stc => !stc.isDeleted);




        }

    }
}