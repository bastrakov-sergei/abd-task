using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.ViewModels.TradePointsManage
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public sealed class EditTradePointViewModel : AddTradePointViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }
    }
}