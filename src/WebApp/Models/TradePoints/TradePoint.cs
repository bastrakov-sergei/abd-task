using System;

// ReSharper disable NotNullMemberIsNotInitialized
namespace WebApp.Models.TradePoints
{
    public sealed class TradePoint
    {
        public Guid Id { get; set; }
        public TradePointType Type { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
    }
}