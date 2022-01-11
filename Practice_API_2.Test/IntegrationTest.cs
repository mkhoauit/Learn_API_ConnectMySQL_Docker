using System.Net.Http;
using FluentAssertions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Practice_API_2.Classes;

namespace Practice_API_2.Test
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;

        public IntegrationTest()
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
            _client = student.CreateClient();
        }
        
        protected async Task AuthenticateAsync ()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Hello");
        }
        /*
        private async Task<string> GetJwtAsync()
        {
            var response = _client.PostAsJsonAsync(,new )
        }
        */
    }
}