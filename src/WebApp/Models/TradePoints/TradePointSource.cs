using System;

// ReSharper disable NotNullMemberIsNotInitialized
namespace WebApp.Models.TradePoints
{
    public sealed class TradePointSource
    {
        public TradePoint TradePoint { get; set; }
        public Guid TradePointSourceId { get; set; }
        public string DataFileType { get; set; }
    }
}