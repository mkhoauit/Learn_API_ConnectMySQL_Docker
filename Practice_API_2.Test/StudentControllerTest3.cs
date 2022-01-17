using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Practice_API_2.Classes;
using Shouldly;
using Xunit;

namespace Practice_API_2.Test
{
    public class StudentControllerTest3
    {
        public readonly DbContextOptions<StudentContext> dbContextOptions;

        public StudentControllerTest3()
        {
            dbContextOptions = new DbContextOptionsBuilder<StudentContext>()
                .UseInMemoryDatabase(databaseName: "TestStudentDb")
                .Options;
        }
        
        [Fact]
        public async Task When_GetAllStudent_It_ShouldBe_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            
            var newStudent1 = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            
            var newStudent2 = new StudentInputDto()
            {
                StudentName = "Thuy",
                Gender = "Female",
                DateOfBirth = DateTime.Parse("1986-09-02")
                
            };
            var newStudent3 = new StudentInputDto()
            {
                StudentName = "Ngoc",
                Gender = "Female",
                DateOfBirth = DateTime.Parse("1986-05-19")
                
            };

            // Act
            await repository.AddStudent(newStudent1);
            await repository.AddStudent(newStudent2);
            await repository.AddStudent(newStudent3);
            var check = repository.GetStudents();
            // Assert
            Assert.Equal(3, await studentContext.Students.CountAsync());
            Assert.Equal(3, check.Result.Count());
        }
        
        [Fact]
        public async Task When_GetStudent_It_ShouldBe_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            int id = 2;
            var newStudent1 = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            
            var newStudent2 = new StudentInputDto()
            {
                StudentName = "Thuy",
                Gender = "Female",
                DateOfBirth = DateTime.Parse("1986-09-02")
                
            };

            // Act
            await repository.AddStudent(newStudent1);
            await repository.AddStudent(newStudent2);
            var check = repository.GetStudent(id);
            // Assert
            Assert.Equal(2, await studentContext.Students.CountAsync());
            Assert.Equal(2, check.Result.StudentId);
        }
        
        [Fact]
        public async Task When_PostStudent_Then_It_Should_Insert_New_Entry()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };

            // Act
            await repository.AddStudent(newStudent);
            var check = repository.GetStudent(1);
        
            // Assert
            Assert.Equal(1, await studentContext.Students.CountAsync());
            check.Result.StudentName.ShouldBe("Khoa");
        }
        
        [Fact]
        public async Task When_Put_Is_Saved_Then_It_Should_Update_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            int id = 1;
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            
            var updateStudent = new StudentInputDto()
            {
                StudentName = "Thuy",
                Gender = "Female",
                DateOfBirth = DateTime.Parse("1986-09-02")
                
            };

            // Act
            await repository.AddStudent(newStudent);
            await repository.UpdateStudent(id,updateStudent);
            var check = repository.GetStudent(id);
            // Assert
            Assert.Equal(1, await studentContext.Students.CountAsync());
            Assert.Equal("Thuy", check.Result.StudentName);
        }
        [Fact]
        public async Task When_Delete_Then_It_ShouldBe_IsDeleted()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            int id = 1;
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            // Act
            await repository.AddStudent(newStudent);
            var delete= await repository.DeleteStudent(id);
            var check = repository.GetStudents();
            // Assert
            Assert.Equal(0, await studentContext.Students.CountAsync());
            check.Result.Should().BeEmpty();
        }
        
        [Fact]
        public async Task When_Post_StudentClass_Then_Get_StudentClass_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            var newSubject = new SubjectInputDto()
            {
                SubjectName = "Math",
                Teacher = "Max"
            };
            await repository.AddStudent(newStudent);
            await repository.AddSubject(newSubject);

            var newStudentClass = new StudentClassDto()
            {
                StudentId = 1,
                SubjectId = 1,
                DateTimeStart = DateTime.Parse("2022-01-01"),
                DateTimeEnd = DateTime.Parse("2022-07-01")
                
            };

            // Act
            await repository.AddStudentClass(newStudentClass);
            var check = repository.GetStudentClass(1,1);

            // Assert
            Assert.Equal(1, await studentContext.StudentClasses.CountAsync());
            Assert.Equal(1, check.Result.StudentId);
        }
        
        [Fact]
        public async Task When_Put_StudentClass_After_PostStudentClass_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            var newSubject = new SubjectInputDto()
            {
                SubjectName = "Math",
                Teacher = "Max"
            };
            await repository.AddStudent(newStudent);
            await repository.AddSubject(newSubject);

            var newStudentClass = new StudentClassDto()
            {
                StudentId = 1,
                SubjectId = 1,
                DateTimeStart = DateTime.Parse("2022-01-01"),
                DateTimeEnd = DateTime.Parse("2022-07-01")
                
            };
            var updateStudentClass = new StudentClassInputDto()
            {
                DateTimeStart = DateTime.Parse("2021-01-01") ,
                DateTimeEnd = DateTime.Now
            };
            await repository.AddStudentClass(newStudentClass);
            // Act
            await repository.UpdateStudentClass(1, 1, updateStudentClass);
            var check = repository.GetStudentClass(1,1);

            // Assert
            Assert.Equal(1, await studentContext.StudentClasses.CountAsync());
            Assert.Equal(DateTime.Parse("2021-01-01"), check.Result.DateTimeStart);
        }
        
        [Fact]
        public async Task When_Delete_StudentClass_After_PostStudentClass_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
                
            };
            var newSubject = new SubjectInputDto()
            {
                SubjectName = "Math",
                Teacher = "Max"
            };
            await repository.AddStudent(newStudent);
            await repository.AddSubject(newSubject);

            var newStudentClass = new StudentClassDto()
            {
                StudentId = 1,
                SubjectId = 1,
                DateTimeStart = DateTime.Parse("2022-01-01"),
                DateTimeEnd = DateTime.Parse("2022-07-01")
                
            };
            
            await repository.AddStudentClass(newStudentClass);
            // Act
            await repository.DeleteStudentClass(1, 1);
            var check = repository.GetStudentClases();

            // Assert
            Assert.Equal(0, await studentContext.StudentClasses.CountAsync());
            check.Result.Should().BeEmpty();
        }
        
    }
}