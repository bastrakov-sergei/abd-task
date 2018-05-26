using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Data
{
    public class AccountDbInitializer
    {
        private const string DefaultUserLogin = "Administrator";
        private const string DefaultUserPassword = "password";
        private const string DefaultUserRole = Roles.Administrator;

        public static async Task Initialize(
            AccountDbContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            ILogger<AccountDbInitializer> logger)
        {
            context.Database.EnsureCreated();
            await CreateDefaultUserAndRoleForApplication(userManager, roleManager, logger).ConfigureAwait(false);
        }

        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm, ILogger<AccountDbInitializer> logger)
        {
            await CreateDefaultRoles(rm, logger).ConfigureAwait(false);
            var user = await CreateDefaultUser(um, logger).ConfigureAwait(false);
            await SetPasswordForDefaultUser(um, logger, user).ConfigureAwait(false);
            await AddDefaultRoleToDefaultUser(um, logger, user).ConfigureAwait(false);
        }

        private static async Task CreateDefaultRoles(RoleManager<IdentityRole> rm, ILogger<AccountDbInitializer> logger)
        {
            foreach (var role in Roles.AsArray)
            {
                if (await rm.Roles.AnyAsync(r => r.Name == role).ConfigureAwait(false))
                    continue;

                logger.LogInformation($"Create the role `{role}` for application");
                var ir = await rm.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);
                if (ir.Succeeded)
                {
                    logger.LogDebug($"Created the role `{role}` successfully");
                }
                else
                {
                    var exception = new ApplicationException($"Default role `{role}` cannot be created");
                    logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                    throw exception;
                }
            }
        }

        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> um, ILogger<AccountDbInitializer> logger)
        {
            var user = await um.FindByNameAsync(DefaultUserLogin).ConfigureAwait(false);
            if (user != null)
                return user;

            logger.LogInformation($"Create default user with login `{DefaultUserLogin}` for application");
            user = new ApplicationUser
            {
                UserName = DefaultUserLogin
            };

            var ir = await um.CreateAsync(user).ConfigureAwait(false);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default user `{DefaultUserLogin}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{DefaultUserLogin}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await um.FindByNameAsync(DefaultUserLogin).ConfigureAwait(false);
            return createdUser;
        }

        private static async Task SetPasswordForDefaultUser(UserManager<ApplicationUser> um, ILogger<AccountDbInitializer> logger, ApplicationUser user)
        {
            if (await um.HasPasswordAsync(user).ConfigureAwait(false))
                return;

            logger.LogInformation($"Set password for default user `{DefaultUserLogin}`");
            var ir = await um.AddPasswordAsync(user, DefaultUserPassword).ConfigureAwait(false);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{DefaultUserPassword}` for default user `{DefaultUserLogin}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{DefaultUserLogin}` cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> um, ILogger<AccountDbInitializer> logger, ApplicationUser user)
        {
            if ((await um.GetRolesAsync(user).ConfigureAwait(false)).Contains(DefaultUserRole))
                return;

            logger.LogInformation($"Add default user `{DefaultUserLogin}` to role '{DefaultUserRole}'");
            var ir = await um.AddToRoleAsync(user, DefaultUserRole).ConfigureAwait(false);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{DefaultUserRole}' to default user `{DefaultUserLogin}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{DefaultUserRole}` cannot be set for the user `{DefaultUserLogin}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            var errors = "";
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}