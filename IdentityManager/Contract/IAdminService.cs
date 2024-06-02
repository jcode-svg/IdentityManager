using IdentityManager.ViewModels;
using System.Security.Claims;

namespace IdentityManager.Contract
{
    public interface IAdminService
    {
        Task<ResponseWrapper<string>> PromoteToAdmin(string userId, ClaimsPrincipal user);
    }
}
