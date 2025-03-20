using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Azure.Messaging;

namespace NewsTest.Tests
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Program>>{
        private readonly WebApplicationFactory<Program> _factory;

        public ApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        // [Theory]
        // [InlineData("/api/news")]
        // public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        // {
        //     // Arrange
        //     var client = _factory.CreateClient();

        //     // Act
        //     var response = await client.GetAsync(url);


        //     // Assert
        //     response.EnsureSuccessStatusCode();
        //     Assert.Equal("text/html; charset=utf-8", 
        //         response.Content.Headers.ContentType.ToString());
            
        // }

    }
}
