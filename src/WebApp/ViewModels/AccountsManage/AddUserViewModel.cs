using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WebApp.Models;

namespace WebApp.ViewModels.AccountsManage
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class AddUserViewModel
    {
        [Required]
        [Display(Name = "Login")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role")]
        [DefaultValue(Roles.Administrator)]
        public string Role { get; set; }
    }
}