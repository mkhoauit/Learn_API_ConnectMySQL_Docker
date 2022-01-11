using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Practice_API_2.Test
{
    
    public class Test_Server
    {
        protected HttpClient Client { get; private set; }

        protected Test_Server()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }

    }
}