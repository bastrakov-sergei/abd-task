using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels.AccountsManage;

namespace WebApp.Services.Account
{
    public sealed class AccountService : IAccountService
    {
        private readonly AccountDbContext accountDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(
            AccountDbContext accountDbContext,
            UserManager<ApplicationUser> userManager)
        {
            this.accountDbContext = accountDbContext;
            this.userManager = userManager;
        }
        public async Task<bool> IsSame(ApplicationUser first, ClaimsPrincipal claimsPrincipal)
        {
            var (second, result) = await GetUserAsync(claimsPrincipal).ConfigureAwait(false);
            if (result == null)
                return false;

            return first.Id == second.Id;
        }

        public async Task<ApplicationUser[]> GetUsersAsync() => await accountDbContext.Users.ToArrayAsync().ConfigureAwait(false);

        public async Task<(ApplicationUser, Error[])> GetUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id).ConfigureAwait(false);
            if (user == null)
                return (null, Error.Create(AccountServiceErrors.UserNotFound));

            return (user, null);
        }

        public async Task<(ApplicationUser, Error[])> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal).ConfigureAwait(false);
            if (user == null)
                return (null, Error.Create(AccountServiceErrors.UserNotFound));

            return (user, null);
        }

        public async Task<(string, Error[])> GetRoleAsync(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            var role = roles.FirstOrDefault();
            if (role == null)
                return (null, Error.Create(AccountServiceErrors.RoleNotAssigned));

            return (role, null);
        }

        public async Task<(ApplicationUser, Error[])> AddAsync(AddUserViewModel viewModel)
        {
            var user = await FindUserByLoginAsync(viewModel.Login).ConfigureAwait(false);
            if (user != null)
                return (null, Error.Create(AccountServiceErrors.UserExists));

            return await accountDbContext.DoTransaction(async () =>
            {
                user = new ApplicationUser
                {
                    UserName = viewModel.Login
                };

                var createResult = await userManager.CreateAsync(user, viewModel.Password).ConfigureAwait(false);
                if (!createResult.Succeeded)
                    return (null, ToError(createResult));

                var roleAssignResult = await userManager.AddToRoleAsync(user, viewModel.Role).ConfigureAwait(false);
                if (!roleAssignResult.Succeeded)
                    return (null, ToError(roleAssignResult));

                var createdUser = await FindUserByLoginAsync(viewModel.Login).ConfigureAwait(false);
                return (createdUser, Error.NoError);
            }).ConfigureAwait(false);
        }

        public async Task<(ApplicationUser, Error[])> UpdateAsync(EditUserViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(viewModel.Id).ConfigureAwait(false);
            if (user == null)
                return (null, Error.Create(AccountServiceErrors.UserNotFound));

            return await accountDbContext.DoTransaction(async () =>
            {
                user.UserName = viewModel.Login;
                user.SecurityStamp = Guid.NewGuid().ToString();
                
                var updateResult = await userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!updateResult.Succeeded)
                    return (null, ToError(updateResult));

                var removePasswordResult = await userManager.RemovePasswordAsync(user).ConfigureAwait(false);
                if (!removePasswordResult.Succeeded)
                    return (null, ToError(removePasswordResult));

                var addPasswordResult = await userManager.AddPasswordAsync(user, viewModel.Password).ConfigureAwait(false);
                if (!addPasswordResult.Succeeded)
                    return (null, ToError(addPasswordResult));

                if (!await userManager.IsInRoleAsync(user, viewModel.Role).ConfigureAwait(false))
                {
                    var roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                    var removeRolesResult = await userManager.RemoveFromRolesAsync(user, roles).ConfigureAwait(false);
                    if (!removeRolesResult.Succeeded)
                        return (null, ToError(removeRolesResult));

                    var roleAssignResult = await userManager.AddToRoleAsync(user, viewModel.Role).ConfigureAwait(false);
                    if (!roleAssignResult.Succeeded)
                        return (null, ToError(roleAssignResult));
                }

                var createdUser = await FindUserByLoginAsync(viewModel.Login).ConfigureAwait(false);
                return (createdUser, Error.NoError);
            }).ConfigureAwait(false);
        }

        [ItemCanBeNull]
        public async Task<Error[]> DeleteAsync(ApplicationUser user) => ToError(await userManager.DeleteAsync(user).ConfigureAwait(false));

        [ItemCanBeNull]
        private async Task<ApplicationUser> FindUserByLoginAsync(string login) => await userManager.FindByNameAsync(login).ConfigureAwait(false);

        [CanBeNull]
        private static Error[] ToError(IdentityResult ir) => ir.Succeeded ? Error.NoError : ir.Errors.Select(e => new Error(e.Code, e.Description)).ToArray();
    }
}
