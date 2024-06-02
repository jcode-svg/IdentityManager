using IdentityManager.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityManager.Contract;

public interface IAccountService
{
    Task<ResponseWrapper<SignInResult>> Login(LoginViewModel model);
    Task Logout();
    Task<ResponseWrapper<IdentityResult>> Register(RegisterViewModel model);
}
