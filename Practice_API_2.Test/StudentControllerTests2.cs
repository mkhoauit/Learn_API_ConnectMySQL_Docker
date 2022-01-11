using System.Collections.Generic;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Practice_API_2.Classes;
using Shouldly;
using Xunit;

namespace Practice_API_2.Test
{
    public class StudentControllerTests2 : IntegrationTest
    {
        
        [Fact]
        public async Task GetAll()
        {
            //Arrange
            var client = new IntegrationTest();
            //Action
            var response = await _client.GetAsync("Student/All");
            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Student>>()).Should().BeEmpty();

        }
        //[Fact]
        public async Task GetByID()
        {
            //Arrange
            await AuthenticateAsync();
            int id = 1;
            //Action
            var response = await _client.GetAsync($"Student/Get/{id}");
            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<Student>()).StudentId.ShouldBe(id);

        }
        
    }
}