using InventoryManagementSystem.BLL.Interfaces;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace InventoryManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateAsync(model.Email, model.Password);
                if (result.Success)
                {
                    var resultAuth = await _userService.AuthenticateAsync(model.Email, model.Password);
                    if (result.Success)
                    {
                        ViewBag.AccessToken = resultAuth.Data;
                        ViewBag.Message = "Account created successfully.";
                        return View("LoginSuccess");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message ?? "An error occurred while creating the account.");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AuthenticateAsync(model.Email, model.Password);
                if (result.Success)
                {
                    ViewBag.AccessToken = result.Data;
                    return View("LoginSuccess");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return View();
        }

    }
}
