namespace WebApp.Models
{
    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string User = "User";

        public static string[] AsArray => new[] {Administrator, User};
    }
}
