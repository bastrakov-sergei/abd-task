using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;

namespace WebApp.Services.DataFiles.Handlers
{
    public sealed class JsonDataFileHandler : IDataFileHandler
    {
        public string DataFileType => "json";

        public Task<bool> CanHandleAsync(DataFile dataFile) => Task.FromResult(dataFile.Name.EndsWith("json"));

        public Task<(IEnumerable<TradePoint>, Error[])> HandleAsync(DataFile dataFile)
        {
            using (var ms = new MemoryStream(dataFile.Content))
            {
                using (var textReader = new StreamReader(ms))
                {
                    using (var jsonReader = new JsonTextReader (textReader))
                    {
                        var serializer = new JsonSerializer();
                        return Task.FromResult((serializer.Deserialize<IEnumerable<TradePoint>>(jsonReader), Error.NoError));
                    }
                }
            }
        }
    }
}