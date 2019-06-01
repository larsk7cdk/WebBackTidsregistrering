using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBackTidsregistrering.Application.Interfaces;
using WebBackTidsregistrering.WebAPI.Models;

namespace WebBackTidsregistrering.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IRegistrationService _registrationService;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationController(UserManager<IdentityUser> userManager, IRegistrationService registrationService,
            ILogger<RegistrationController> logger)
        {
            _userManager = userManager;
            _registrationService = registrationService;
            _logger = logger;
        }

        /// <summary>
        ///     Viser alle tidsregistreringer for en bruger
        /// </summary>
        /// <returns>Registreringer</returns>
        /// <response code="200">Returner tidsregistreringer</response>
        [Consumes("application/json", "application/hal+json")]
        [Produces("application/json", "application/hal+json")]
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<IActionResult> GetRegistrations()
        {
            _logger.LogInformation("Vis registreringer");

            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type.EndsWith("emailaddress"))?.Value;
            var user = await _userManager.FindByNameAsync(email);
            var data = _registrationService.GetAllByIdAsync(user.Id).ToList();

            var result = data.Select(s => new RegistrationsModel
            {
                Id = s.Id,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime.GetValueOrDefault()
            });


            var responseConfig = new HALModelConfig
            {
                LinkBase = $"{Request.Scheme}://{Request.Host.ToString()}",
                ForceHAL = Request.ContentType == "application/hal+json"
            };

            var response = new HALResponse(responseConfig);
            response.AddEmbeddedCollection("registrations", result);
            response.AddLinks(
                new Link("self", "/api/registration/", null, "GET")
            );

            return this.HAL(response);
        }
        
        /// <summary>
        ///     Vis detaljer for registrering
        /// </summary>
        /// <returns>Registreringer</returns>
        /// <response code="200">Returner detaljer for registrering ved angivet id</response>
        [Consumes("application/json", "application/hal+json")]
        [Produces("application/json", "application/hal+json")]
        [ProducesResponseType(200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration([Required]int id)
        {
            _logger.LogInformation($"Vis detaljer for registrering {id}");

            var data = await _registrationService.GetByIdAsync(id);

            var result =  new RegistrationsModel
            {
                Id = data.Id,
                Date = data.Date,
                StartTime = data.StartTime,
                EndTime = data.EndTime.GetValueOrDefault()
            };


            var responseConfig = new HALModelConfig
            {
                LinkBase = $"{Request.Scheme}://{Request.Host.ToString()}",
                ForceHAL = Request.ContentType == "application/hal+json"
            };

            var response = new HALResponse(responseConfig);
            response.AddEmbeddedResource("registration", result);
            response.AddLinks(
                new Link("self", $"/api/registration/{id}", null, "GET")
            );

            return this.HAL(response);
        }
    }
}