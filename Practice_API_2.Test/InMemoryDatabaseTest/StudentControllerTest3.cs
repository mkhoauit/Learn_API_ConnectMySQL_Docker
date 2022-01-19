using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice_API_2.Classes;
using Practice_API_2.Controllers;
using Shouldly;
using Xunit;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;

namespace Practice_API_2.Test.InMemoryDatabaseTest
{
    public class StudentControllerTest3 : IntegrationTest
    {
        [Fact]
        public async Task When_GetAllStudent_It_ShouldBe_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            
            // Act
            var check = repository.GetStudents();
            var checkStatus = await controller.GetStudents();
           
            // Assert
            Assert.Equal(2, await studentContext.Students.CountAsync());
            Assert.Equal(2, check.Result.Count());
            Assert.IsType<OkObjectResult>(checkStatus.Result);
        }
        
        [Fact]
        public async Task When_GetStudent_WrongId_ShouldBe_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            int id = 4;
            
            // Act
            var checkStatus= await controller.GetStudent(id);

            // Assert
            var status= checkStatus.Result;
            Assert.IsType<NotFoundResult>(status);
        }
        
        [Fact]
        public async Task When_GetStudent_RightId_ShouldBe_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            int id = 2;
            
            // Act
            var check = repository.GetStudent(id);
            
            // Assert
            Assert.Equal(id, check.Result.StudentId);
           
        }
        
        [Fact]
        public async Task When_PostStudent_It_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            var newStudent = new StudentInputDto()
            {
                StudentName = "Khoa",
                Gender = "Male",
                DateOfBirth = DateTime.Parse("1986-01-01")
            };

            // Act
            var add= await repository.AddStudent(newStudent);
            var check = repository.GetStudent(add.StudentId);
            // Assert
            Assert.Equal(3, await studentContext.Students.CountAsync());
            check.Result.StudentName.ShouldBe("Khoa");
            
            var checkController = await controller.CreateStudent(newStudent);
            Assert.IsType<CreatedAtActionResult>(checkController.Result);
        }
        
        [Fact]
        public async Task When_PutStudent_RightId_Should_Update_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            int idUpdate = 1;

            var updateStudent = new StudentInputDto()
            {
                StudentName = "Thuy",
                Gender = "Female",
                DateOfBirth = DateTime.Parse("1986-09-02")
                
            };

            // Act
            var update= await repository.UpdateStudent(idUpdate,updateStudent);
            var check = repository.GetStudent(update.StudentId);
            var checkController = await controller.UpdateStudent(idUpdate, updateStudent);
            // Assert
            Assert.Equal("Thuy", check.Result.StudentName);
            Assert.IsType<OkObjectResult>(checkController.Result);
        }
        
        [Fact]
        public async Task When_PutStudent_WrongId_ShouldBe_()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            int idUpdate = 99;
            var updateStudent = new StudentInputDto()
            {
                StudentName = "Thuy",
                Gender = "Female",
                DateOfBirth = DateTime.Parse("1986-09-02")
                
            };

            // Act
            var update= await repository.UpdateStudent(idUpdate,updateStudent);
            var check = await controller.UpdateStudent(idUpdate, updateStudent);

            // Assert
            Assert.IsType<NotFoundObjectResult>(check.Result);
        }
        
        [Fact]
        public async Task When_DeleteStudent_WrongId_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            int id = 9;
            
            // Act
            var deleteController = await controller.SoftDeleteStudent(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(deleteController.Result);
        }
        
        [Fact]
        public async Task When_DeleteStudent_RightId_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            int id = 2;
            
            // Act
            var deleteController = await controller.SoftDeleteStudent(id);
            var checkController = await controller.GetStudent(id);
            
            // Assert
            Assert.IsType<OkObjectResult>(deleteController.Result);
            Assert.IsType<NotFoundResult>(checkController.Result);
        }
        
        [Fact]
        public async Task When_Post_StudentClass_In_Controller_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            
            var newStudentClass = new StudentClassDto()
            {
                StudentId = 2,
                SubjectId = 2,
                DateTimeStart = DateTime.Parse("2022-01-01"),
                DateTimeEnd = DateTime.Parse("2022-07-01")
            };

            // Act
            var checkController = await controller.CreateStudentClass(newStudentClass);

            // Assert
            Assert.IsType<CreatedAtActionResult>(checkController.Result);

        }
        
        [Fact]
        public async Task When_Post_StudentClass_In_Repository_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
           
            var newStudentClass = new StudentClassDto()
            {
                StudentId = 2,
                SubjectId = 2,
                DateTimeStart = DateTime.Parse("2022-01-01"),
                DateTimeEnd = DateTime.Parse("2022-07-01")
            };

            // Act
            await repository.AddStudentClass(newStudentClass);

            // Assert
            Assert.Equal(2, await studentContext.StudentClasses.CountAsync());
        }
        
        [Fact]
        public async Task When_Put_StudentClass_WrongId_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            var updateStudentClass = new StudentClassInputDto()
            {
                DateTimeStart = DateTime.Parse("2021-01-01") ,
                DateTimeEnd = DateTime.Now
            };
           
            // Act
            var checkController = await controller.UpdateStudentClass(9, 9, updateStudentClass);

            // Assert
            Assert.IsType<NotFoundObjectResult>(checkController.Result);
        }
        
        [Fact]
        public async Task When_Put_StudentClass_RightId_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            var updateStudentClass = new StudentClassInputDto()
            {
                DateTimeStart = DateTime.Parse("2021-01-01") ,
                DateTimeEnd = DateTime.Now
            };
           
            // Act
            var checkController = await controller.UpdateStudentClass(1, 1, updateStudentClass);
            var check = repository.GetStudentClass(1,1);

            // Assert
            Assert.Equal(1, await studentContext.StudentClasses.CountAsync());
            Assert.Equal(DateTime.Parse("2021-01-01"), check.Result.DateTimeStart);
            Assert.IsType<OkObjectResult>(checkController.Result);
        }
        
        [Fact]
        public async Task When_Delete_StudentClass_WrongId_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            
            // Act
            var checkController = await controller.SoftDeleteStudentClass(9, 9);
            
            // Assert
            Assert.IsType<NotFoundObjectResult>(checkController.Result);
        }
        
        [Fact]
        public async Task When_Delete_StudentClass_RightId_ShouldBe()
        {
            // Arrange
            var studentContext = new StudentContext(dbContextOptions);
            StudentRepository repository = new StudentRepository(studentContext);
            StudentController controller = new StudentController(repository);
            
            // Act
            var checkController = await controller.SoftDeleteStudentClass(1, 1);
            var check = repository.GetStudentClass(1,1);

            // Assert
            Assert.IsType<OkObjectResult>(checkController.Result);
            check.Result.Should().BeNull();
        }
        
    }
}