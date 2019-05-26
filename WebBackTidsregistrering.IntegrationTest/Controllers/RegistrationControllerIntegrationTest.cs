using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebBackTidsregistrering.WebUI;
using Xunit;

namespace WebBackTidsregistrering.IntegrationTests.Controllers
{
    public class RegistrationControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public RegistrationControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

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