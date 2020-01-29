using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SSApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SSApp.IntegrationTests
{
    public class BasicTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BasicTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Alterations/Index")]
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
        [Theory]
        [ClassData(typeof(TestData))]
        public async Task Create_EndpointsReturnSuccessAndCorrectContentType(Alteration a)
        {
            // Arrange
            var client = _factory.CreateClient();

            var getResponse = await client.GetAsync("/Alterations/Create");
            getResponse.EnsureSuccessStatusCode();
            string antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(getResponse);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/Alterations/Create");
            var content = new Dictionary<string, string>()
            {
                { "__RequestVerificationToken", antiForgeryToken},
                { "alteration", JsonConvert.SerializeObject(a)}
            };
            var formContent = new FormUrlEncodedContent(content);
            request.Content = formContent;

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Theory]
        [InlineData("/Alterations/Edit/1")]
        [InlineData("/Alterations/Edit/2")]
        public async Task Edit_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();
            var a = new Alteration()
            {
                Id = 1,
                Status = StatusEnum.Paid,
            };
            var getResponse = await client.GetAsync(url);
            getResponse.EnsureSuccessStatusCode();
            string antiForgeryToken = await AntiForgeryHelper.ExtractAntiForgeryToken(getResponse);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new Dictionary<string, string>()
            {
                { "__RequestVerificationToken", antiForgeryToken},
                { "Id", a.Id.ToString()},
                { "Status", StatusEnum.Paid.ToString()},

            };
            var formContent = new FormUrlEncodedContent(content);
            request.Content = formContent;

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
