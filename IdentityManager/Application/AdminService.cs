using IdentityManager.Contract;
using IdentityManager.Models;
using IdentityManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityManager.Application;

public class AdminService : IAdminService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminService(UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ResponseWrapper<string>> PromoteToAdmin(string userId, ClaimsPrincipal currentUser)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return ResponseWrapper<string>.Error("User ID cannot be null or empty");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return ResponseWrapper<string>.Error($"User with ID {userId} not found");
        }

        var currentUserId = _userManager.GetUserId(currentUser);

        if (userId == currentUserId)
        {
            return ResponseWrapper<string>.Error("You cannot promote yourself to Admin");
        }

        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!roleResult.Succeeded)
            {
                return ResponseWrapper<string>.Error("Failed to create Admin role");
            }
        }

        var result = await _userManager.AddToRoleAsync(user, "Admin");
        if (result.Succeeded)
        {
            return ResponseWrapper<string>.Success($"User {user.UserName} has been promoted to Admin");
        }
        else
        {
            return ResponseWrapper<string>.Error($"User {user.UserName} could not be promoted to Admin");
        }
    }
}
