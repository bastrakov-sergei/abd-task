using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ViewModels.AccountsManage
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class EditUserViewModel : AddUserViewModel
    {
        [HiddenInput]
        [Required]
        public string Id { get; set; }
    }
}
