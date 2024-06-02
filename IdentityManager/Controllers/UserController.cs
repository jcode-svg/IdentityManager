using IdentityManager.Contract;
using IdentityManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityManager.Controllers;

public class UserController : Controller
{
    private readonly IAccountService _accountService;

    public UserController(IAccountService accountService)
    {
        _accountService = accountService;
    }

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
            var result = await _accountService.Register(model);
            if (result.IsSuccessful && result.ResponseObject.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in JsonConvert.DeserializeObject<IEnumerable<IdentityError>>(result.Message))
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountService.Login(model);
            if (result.IsSuccessful && result.ResponseObject.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _accountService.Logout();
        return RedirectToAction("Index", "Home");
    }
}