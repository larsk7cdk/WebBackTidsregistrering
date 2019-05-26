using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WebBackTidsregistrering.WebUI;
using Xunit;

namespace WebBackTidsregistrering.IntegrationTests
{
    public class UrlTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UrlTest(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_IndexToDo_Schould_ReturnIndexWithSuccess()
        {
            // Arrange
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            //Act

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}