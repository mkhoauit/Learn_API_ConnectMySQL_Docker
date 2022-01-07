using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Practice_API_2.Classes;
using Practice_API_2.Controllers;
using Practice_API_2.Interfaces;
using Xunit;
using Shouldly;
using static System.Threading.Tasks.Task;

namespace Practice_API_2.Test
{
    public class StudentControllertest
    {
        private Mock<IStudentRepository> studentsRepoMock;
        private StudentController _controller;

        public StudentControllertest()
        {
            studentsRepoMock = new Mock<IStudentRepository>();
            _controller = new StudentController(studentsRepoMock.Object);
        }
        [Fact]
        public async Task Test1()
        {
            //Arrange
            var mockSubjectList = new List<Subject>
            {
                new Subject{SubjectName = "History",Teacher = "Lou Yi"},
                new Subject{SubjectName = "Biology", Teacher = "Hulk"}
            };
            studentsRepoMock
                .Setup(repo => repo.GetSubjects())
                .Returns(Task.FromResult(mockSubjectList));
            //Action
            var result = await _controller.GetSubjects();
            //Assert
            var checkResult = Assert.IsType<OkResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Subject>>(typeof(OkResult));
            Assert.Equal(2,model.Count());
            
        }
    }
}