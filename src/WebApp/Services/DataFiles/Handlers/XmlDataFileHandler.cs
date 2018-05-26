using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;

namespace WebApp.Services.DataFiles.Handlers
{
    public sealed class XmlDataFileHandler : IDataFileHandler
    {
        public string DataFileType => "xml";

        public Task<bool> CanHandleAsync(DataFile dataFile) => Task.FromResult(dataFile.Name.EndsWith("xml"));

        public Task<(IEnumerable<TradePoint>, Error[])> HandleAsync(DataFile dataFile)
        {
            using (var ms = new MemoryStream(dataFile.Content))
            {
                using (var textReader = new StreamReader(ms))
                {
                    var xmlSerializer = new XmlSerializer(typeof(TradePoint[]),
                        new[] {typeof(TradePoint), typeof(Location), typeof(TradePointType)});

                    var tradePoints = (IEnumerable<TradePoint>) xmlSerializer.Deserialize(textReader);
                    return Task.FromResult((tradePoints, Error.NoError));
                }
            }
        }
    }
}