using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebBackTidsregistrering.Application.Interfaces;
using WebBackTidsregistrering.Domain.Entities;
using WebBackTidsregistrering.WebUI.ViewModels.Registration;

namespace WebBackTidsregistrering.WebUI.Controllers
{
    public class RegistrationController : Controller
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


        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Vis registreringer");

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var data = _registrationService.GetAllByIdAsync(user.Id).ToList();

            var models = data.Select(s => new RegistrationsViewModel
            {
                Id = s.Id,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime.GetValueOrDefault()
            });

            return View(models);
        }

        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation($"Vis detaljer for registrering {id}");

            var data = await _registrationService.GetByIdAsync(id);

            var model = new RegistrationsViewModel
            {
                Id = data.Id,
                Date = data.Date,
                StartTime = data.StartTime,
                EndTime = data.EndTime.GetValueOrDefault()
            };

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new RegistrationViewModel
            {
                Date = DateTime.Today,
                StartTime = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Date", "StartTime", "EndTime")] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _logger.LogInformation("Opret registrering");

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var entity = new Registration
            {
                UserId = user.Id,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            await _registrationService.CreateAsync(entity);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var data = await _registrationService.GetByIdAsync(id);

            var model = new RegistrationViewModel
            {
                Id = data.Id,
                UserId = data.UserId,
                Date = data.Date,
                StartTime = data.StartTime,
                EndTime = data.EndTime.GetValueOrDefault()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind("Id", "UserId", "Date", "StartTime", "EndTime")]
            RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _logger.LogInformation($"Opdater registrering {model.Id}");

            var entity = new Registration
            {
                Id = model.Id,
                UserId = model.UserId,
                Date = model.Date,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            await _registrationService.UpdateAsync(entity);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var data = await _registrationService.GetByIdAsync(id);

            var model = new RegistrationsViewModel
            {
                Id = data.Id,
                Date = data.Date,
                StartTime = data.StartTime,
                EndTime = data.EndTime.GetValueOrDefault()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete([Bind("Id", "UserId", "Date", "StartTime", "EndTime")]
            RegistrationViewModel model)
        {
            _logger.LogInformation($"Slet registrering {model.Id}");

            await _registrationService.DeleteAsync(model.Id);

            return RedirectToAction("Index");
        }
    }
}