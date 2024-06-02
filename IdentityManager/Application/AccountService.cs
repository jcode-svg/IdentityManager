using IdentityManager.Contract;
using IdentityManager.Models;
using IdentityManager.ViewModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace IdentityManager.Application;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ResponseWrapper<IdentityResult>> Register(RegisterViewModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return ResponseWrapper<IdentityResult>.Error(JsonConvert.SerializeObject(result.Errors));
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return ResponseWrapper<IdentityResult>.Success(result, "");
    }

    public async Task<ResponseWrapper<SignInResult>> Login(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return ResponseWrapper<SignInResult>.Error("Invalid login attempt.");
        }

        return ResponseWrapper<SignInResult>.Success(result, "");
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}

