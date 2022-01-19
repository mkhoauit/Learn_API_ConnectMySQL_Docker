using System;
using Microsoft.EntityFrameworkCore;
using Practice_API_2.Classes;

namespace Practice_API_2.Test.InMemoryDatabaseTest
{
    public class IntegrationTest : IDisposable
    {
        public StudentContext _context { get; private set; }
        public readonly DbContextOptions<StudentContext> dbContextOptions;

        public IntegrationTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<StudentContext>()
                .UseInMemoryDatabase(databaseName: "TestStudentDb")
                .Options;

            _context = new StudentContext(dbContextOptions);
            //Students
            _context.Students.Add(new Student { StudentId = 1, StudentName = "Tow", Gender = "Male", DateOfBirth = DateTime.Parse("1986-01-01") ,isDeleted = false});
            _context.Students.Add(new Student { StudentId = 2, StudentName = "Tylor", Gender = "Female", DateOfBirth = DateTime.Parse("1987-05-01"),isDeleted = false});
            _context.SaveChanges();
            //Subject
            _context.Subjects.Add(new Subject {SubjectId = 1, SubjectName = "Chemistry", Teacher = "Peter", isDeleted = false});
            _context.Subjects.Add(new Subject {SubjectId = 2, SubjectName = "Math", Teacher = "Timmy", isDeleted = false});
            _context.SaveChanges();
            //StudentClass
            _context.StudentClasses.Add( new StudentClass {StudentId = 1,SubjectId = 1, DateTimeStart = DateTime.Parse("2022-01-01") , DateTimeEnd = DateTime.Parse("2022-07-01"), isDeleted = false});
            _context.SaveChanges();
        }
       
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}