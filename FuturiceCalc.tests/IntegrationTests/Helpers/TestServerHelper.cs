using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FuturiceCalc.tests.IntegrationTests.Helpers
{
    /// <summary>
    /// Helper for building a test server and making http requests to this server
    /// </summary>
    public static class TestServerHelper
    {
        public static HttpResponseMessage GetCalculusResponse(HttpClient client, string encodedExpression)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                $"/api/evaluate-expression/{encodedExpression}");
          
            var response = client.SendAsync(request).Result;
            return response;
        }

        public static IHostBuilder GetHost()
        {
            var hostBuilder = new HostBuilder()
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.test.json", optional: true);
                    config.AddEnvironmentVariables();

                })
                .ConfigureWebHost(webHost =>
                {
                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<Startup>();
                });
            return hostBuilder;
        }
    }
}
