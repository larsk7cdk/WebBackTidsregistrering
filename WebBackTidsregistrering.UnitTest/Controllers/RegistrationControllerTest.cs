using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebBackTidsregistrering.Application.Interfaces;
using WebBackTidsregistrering.Domain.Entities;
using WebBackTidsregistrering.WebUI.Controllers;
using WebBackTidsregistrering.WebUI.ViewModels.Registration;
using Xunit;

namespace WebBackTidsregistrering.UnitTest.Controllers
{
    public class RegistrationControllerTest
    {
        private readonly IReadOnlyList<Registration> _registrationsTestDouble = new List<Registration>
        {
            new Registration
            {
                Id = 1,
                UserId = "1",
                Date = new DateTime(2019, 1, 1),
                StartTime = new DateTime(2019, 1, 1),
                EndTime = new DateTime(2019, 1, 1, 16, 0, 0)
            },
            new Registration
            {
                Id = 2,
                UserId = "1",
                Date = new DateTime(2019, 1, 2),
                StartTime = new DateTime(2019, 1, 1),
                EndTime = new DateTime(2019, 1, 2, 16, 0, 0)
            }
        };

        private Mock<ILogger<RegistrationController>> _mockLogger;
        private Mock<IRegistrationService> _mockRegistrationService;

        private RegistrationController SetupController()
        {
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            var mockUserManager =
                new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            var mockHttpContext = new Mock<HttpContext>();
            _mockLogger = new Mock<ILogger<RegistrationController>>();

            _mockRegistrationService = new Mock<IRegistrationService>();

            mockHttpContext.Setup(s => s.User.Identity.Name).Returns("");
            mockUserManager.Setup(s => s.FindByNameAsync("")).ReturnsAsync(new IdentityUser());

            var controller =
                new RegistrationController(mockUserManager.Object, _mockRegistrationService.Object, _mockLogger.Object)
                {
                    ControllerContext = new ControllerContext { HttpContext = mockHttpContext.Object }
                };

            return controller;
        }

        [Fact]
        public async Task RegistrationController_Index_Schould_ReturnsAViewResult_WithAListOfRegistrations()
        {
            // Arrange
            var controller = SetupController();

            _mockRegistrationService.Setup(s => s.GetAllByIdAsync(It.IsAny<string>()))
                .Returns(_registrationsTestDouble);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<RegistrationsViewModel>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task RegistrationController_Create_Schould_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var controller = SetupController();

            _mockRegistrationService.Setup(s => s.GetAllByIdAsync(It.IsAny<string>()))
                .Returns(_registrationsTestDouble);

            controller.ModelState.AddModelError("Name", "Navn skal være udfyldt.");
            var model = new RegistrationViewModel();

            // Act
            var result = await controller.Create(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task TodoController_Create_Schould_ReturnsRedirect_WhenModelStateIsValid()
        {
            // Arrange
            var controller = SetupController();

            _mockRegistrationService.Setup(s => s.GetAllByIdAsync(It.IsAny<string>()))
                .Returns(_registrationsTestDouble);

            RegistrationViewModel model = new RegistrationViewModel
            {
                Id = 1,
                UserId = "1",
                Date = new DateTime(2019, 1, 1),
                StartTime = new DateTime(2019, 1, 1),
                EndTime = new DateTime(2019, 1, 1, 16, 0, 0)
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRegistrationService.Verify();
        }
    }
}