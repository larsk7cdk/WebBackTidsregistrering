﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using WebBackTidsregistrering.Application.Interfaces;
using WebBackTidsregistrering.WebUI.ViewModels.Account;

namespace WebBackTidsregistrering.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        // REIGSTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _emailService.SendEmail(user.Email,
                        "Velkommen til Tidsregistrering",
                        "Velkomst meddelelse til medarbejderen!");
                    return RedirectToAction(nameof(Login), "Account");
                }

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.CurrentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()
                .RequestCulture.Culture.ToString();

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewBag.CurrentCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>()
                .RequestCulture.Culture.ToString();

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false,
                    false);

                if (result.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction(nameof(RegistrationController.Index), "Registration");
                }

                ModelState.AddModelError(string.Empty, "Login forsøg mislykkedes.");
            }

            return View(model);
        }

        // LOGOUT
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SetCulture([FromQuery] string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions {Expires = DateTimeOffset.UtcNow.AddYears(1)}
            );

            ViewBag.CurrentCulture = culture;

            return View("Login");
        }
    }
}