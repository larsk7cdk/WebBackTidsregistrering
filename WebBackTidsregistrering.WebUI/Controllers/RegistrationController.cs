using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            _logger.LogInformation("Test");

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

        // GET: Registration/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation("Test");

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

        // GET: Registration/Create
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

        // GET: Registration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Registration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registration/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}