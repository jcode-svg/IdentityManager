using IdentityManager.Contract;
using IdentityManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace IdentityManager.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string userId)
    {
        var result = await _adminService.PromoteToAdmin(userId, User);
        if (result.IsSuccessful)
        {
            ViewBag.Message = "Successful";
            return View();
        }

        ModelState.AddModelError(string.Empty, result.Message);

        return View();
    }
}
