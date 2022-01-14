using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task GetSubject_ById_ShouldBe()
        {
            //Arrange
            var subjectId = 1;
            var subjectName = "Math";
            var subjectTeacher = "Ming";
            var subjectDto = new SubjectDto()
            {
                SubjectId = subjectId,
                SubjectName = subjectName,
                Teacher = subjectTeacher
            };
            studentsRepoMock
                .Setup(x => x.GetSubject(subjectId));
                //.Returns(Task.FromResult<SubjectDto>( subjectDto));
            
            //Action
            var result = await _controller.GetSubject(subjectId);
            
            //Assert
            Assert.Equal(subjectId,result.Value.SubjectId);


        }
        [Fact]
        public async Task GetSubject_ById_ShouldBeNotFound()
        {
            //Arrange
            var subjectId = 91;

            studentsRepoMock
                .Setup(x => x.GetSubject(subjectId))
                .Returns(Task.FromResult<Subject>(null));
            
            //Action
            var result = await _controller.GetSubject(subjectId);
            
            //Assert
            //Assert.IsType<NotFoundResult>(result);
            result.Value.Teacher.ShouldBe(null);


        }

        [Fact]
        public async Task GetSubject_All_ShouldBe()
        {
            //Arrange
           
            var mockSubjectList = new List<Subject>
            {
                new Subject{SubjectId = 10,SubjectName = "History",Teacher = "Lou Yi",isDeleted = false},
                new Subject{SubjectId = 11,SubjectName = "Biology", Teacher = "Hulk",isDeleted = false}
            };
            studentsRepoMock
                .Setup(repo => repo.GetSubjects());
                //.ReturnsAsync(Task.FromResult(mockSubjectList));
            //Action
            var result = await _controller.GetSubjects();
            //Assert
            var checkResult = Assert.IsType<OkResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Subject>>(typeof(OkResult));
            Assert.Equal(2,model.Count());
            
        }
        
    }
}