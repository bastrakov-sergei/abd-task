using System.Diagnostics.CodeAnalysis;
using WebApp.Models;

namespace WebApp.ViewModels
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public Error[] Errors { get; set; }
    }
}