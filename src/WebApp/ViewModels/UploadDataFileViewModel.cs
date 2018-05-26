using System.Diagnostics.CodeAnalysis;

namespace WebApp.ViewModels
{
    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized")]
    public class UploadDataFileViewModel
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
