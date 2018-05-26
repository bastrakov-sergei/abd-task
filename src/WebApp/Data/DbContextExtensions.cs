using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public static class DbContextExtensions
    {
        public static async Task<(TResult, Error[])> DoTransaction<TResult>(this DbContext context, Func<Task<(TResult, Error[])>> action)
        {
            using (var identitydbContextTransaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                var (result, e) = await action().ConfigureAwait(false);
                try
                {
                    if (e == Error.NoError)
                        identitydbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    identitydbContextTransaction.Rollback();
                }
                return (result, e);
            }
        }
    }
}