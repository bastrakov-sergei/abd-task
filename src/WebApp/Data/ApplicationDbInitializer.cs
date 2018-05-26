using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.TradePoints;

namespace WebApp.Data
{
    public class ApplicationDbInitializer
    {
        private static readonly TradePointType[] DefaultTypes =
        {
            new TradePointType
            {
                Id = Guid.Parse("DDA7C744-EB5E-441E-B6E6-5BCAF7F16D96"),
                Name = "ATM"
            },
            new TradePointType
            {
                Id = Guid.Parse("150C56A1-92E6-45A8-B40D-ACECF5B46326"),
                Name = "Office"
            },
            new TradePointType
            {
                Id = Guid.Parse("78D6D702-F821-4296-849B-B9F4426A2ED3"),
                Name = "InfoKiosk"
            }
        };

        public static async Task Initialize(ApplicationDbContext context)
        {
            foreach (var type in DefaultTypes)
            {
                if (!await context.TradePointTypes.AnyAsync(tp => tp.Id == type.Id).ConfigureAwait(false))
                    await context.TradePointTypes.AddAsync(type).ConfigureAwait(false);
            }
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}