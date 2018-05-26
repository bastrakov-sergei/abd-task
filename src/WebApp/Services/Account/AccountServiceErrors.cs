using WebApp.Models;

namespace WebApp.Services.Account
{
    public static class AccountServiceErrors
    {
        public static Error UserExists => new Error(nameof(UserExists), "User with specified login exists");
        public static Error UserNotFound => new Error(nameof(UserNotFound), "User with specified id is not found");
        public static Error RoleNotAssigned => new Error(nameof(RoleNotAssigned), "Role is not assigned to user");
    }
}