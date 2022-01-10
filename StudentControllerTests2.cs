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
    public class StudentControllerTests2 
    {
        private readonly HttpClient _client;

        public StudentControllerTests2()
        {
            var student= new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(StudentContext));
                        services.AddDbContext<StudentContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            _client = student.CreateDefaultClient();
        }
        [Fact]
        public async Task GetAll()
        {
            //Arrange
            
            //Action
            var response = await _client.GetAsync("Student/All");
            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Student>>()).Should().BeEmpty();

        }
    }
}