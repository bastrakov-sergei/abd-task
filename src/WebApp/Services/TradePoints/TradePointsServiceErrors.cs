using WebApp.Models;

namespace WebApp.Services.TradePoints
{
    public static class TradePointsServiceErrors
    {
        public static Error TradePointExists => new Error(nameof(TradePointExists), "Trade point with specified name is exists");
        public static Error TradePointTypeNotFound => new Error(nameof(TradePointTypeNotFound), "Trade point type with specified id not found");
        public static Error TradePointNotFound => new Error(nameof(TradePointNotFound), "Trade point with specified id not found");
    }
}