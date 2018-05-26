using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WebApp.Models;
using WebApp.ViewModels.AccountsManage;

namespace WebApp.Services.Account
{
    public interface IAccountService
    {
        Task<bool> IsSame(ApplicationUser first, ClaimsPrincipal claimsPrincipal);
        Task<ApplicationUser[]> GetUsersAsync();
        Task<(ApplicationUser, Error[])> GetUserAsync(string id);
        Task<(ApplicationUser, Error[])> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<(string, Error[])> GetRoleAsync(ApplicationUser user);
        Task<(ApplicationUser, Error[])> AddAsync(AddUserViewModel viewModel);
        Task<(ApplicationUser, Error[])> UpdateAsync(EditUserViewModel viewModel);
        [ItemCanBeNull]
        Task<Error[]> DeleteAsync(ApplicationUser user);
    }
}