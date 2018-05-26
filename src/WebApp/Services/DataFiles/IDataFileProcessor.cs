using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WebApp.Models;

namespace WebApp.Services.DataFiles
{
    public interface IDataFileProcessor
    {
        [ItemCanBeNull]
        Task<Error[]> HandleAsync(Guid dataFileId);
    }
}