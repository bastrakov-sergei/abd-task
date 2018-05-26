using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;

namespace WebApp.Services.DataFiles
{
    public interface IDataFileHandler
    {
        string DataFileType { get; }
        Task<bool> CanHandleAsync(DataFile dataFile);
        Task<(IEnumerable<TradePoint>, Error[])> HandleAsync(DataFile dataFile);
    }
}