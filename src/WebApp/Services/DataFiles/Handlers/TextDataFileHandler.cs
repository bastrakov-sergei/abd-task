using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;

namespace WebApp.Services.DataFiles.Handlers
{
    public sealed class TextDataFileHandler : IDataFileHandler
    {
        public string DataFileType => "txt";

        public Task<bool> CanHandleAsync(DataFile dataFile) => Task.FromResult(dataFile.Name.EndsWith("txt"));

        public async Task<(IEnumerable<TradePoint>, Error[])> HandleAsync(DataFile dataFile)
        {
            var tradePoints = new List<TradePoint>();
            using (var ms = new MemoryStream(dataFile.Content))
            {
                using (var textReader = new StreamReader(ms))
                {
                    var lineNumber = 0;
                    try
                    {
                        string line;
                        while ((line = await textReader.ReadLineAsync().ConfigureAwait(false)) != null)
                        {
                            var map = new Dictionary<string, string>();
                            var expressions = line.TrimEnd(';').Split(';');
                            foreach (var expression in expressions)
                            {
                                var property = expression.Split('=');
                                map[property[0]] = property[1];
                            }

                            var tradePoint = new TradePoint
                            {
                                Id = Guid.Parse(map[nameof(TradePoint.Id)]),
                                Name = map[nameof(TradePoint.Name)],
                                Type = new TradePointType
                                {
                                    Id = Guid.Parse(map[$"Type{nameof(TradePointType.Id)}"]),
                                    Name = map[$"Type{nameof(TradePointType.Name)}"]
                                },
                                Location = new Location
                                {
                                    Latitude = double.Parse(map[nameof(Location.Latitude)]),
                                    Longitude = double.Parse(map[nameof(Location.Longitude)])
                                }
                            };
                            tradePoints.Add(tradePoint);
                            lineNumber++;
                        }
                    }
                    catch (Exception)
                    {
                        return (null, Error.Create("InvalidSyntax", $"Invalid syntax at line {lineNumber}"));
                    }

                    return (tradePoints, Error.NoError);
                }
            }
        }
    }
}