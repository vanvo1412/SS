using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SSApp.IntegrationTests
{
    public class BasicTests : IClassFixture<WebApplicationE2EFactory>
    {
        private readonly WebApplicationE2EFactory _factory;

        public BasicTests(WebApplicationE2EFactory factory)
        {
            _factory = factory;
        }

        [Theory]
        //[InlineData("/Index")]
        //[InlineData("/Create")]
        [InlineData("/Edit")]
        //[InlineData("/Delete")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
