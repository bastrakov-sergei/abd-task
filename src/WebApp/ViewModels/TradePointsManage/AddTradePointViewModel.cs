using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApp.ViewModels.TradePointsManage
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class AddTradePointViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "TypeId")]
        public Guid TypeId { get; set; }
        [Required]
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Required]
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
    }
}
