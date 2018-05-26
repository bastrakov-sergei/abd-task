using System;
using JetBrains.Annotations;

// ReSharper disable NotNullMemberIsNotInitialized
namespace WebApp.Models.DataFiles
{
    public sealed class DataFile
    {
        public Guid Id { get; set; }
        public byte[] Hash { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        [CanBeNull]
        public string Type { get; set; }
        public bool IsProcessed { get; set; }
        [CanBeNull]
        public string ProcessingError { get; set; }
    }
}
