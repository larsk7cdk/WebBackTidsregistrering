using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebBackTidsregistrering.WebUI;
using Xunit;

namespace WebBackTidsregistrering.IntegrationTests.Controllers
{
    public class RegistrationControllerUrlTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public RegistrationControllerUrlTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private readonly WebApplicationFactory<Startup> _factory;

        [Theory]
        [InlineData("/Registration/Create")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}