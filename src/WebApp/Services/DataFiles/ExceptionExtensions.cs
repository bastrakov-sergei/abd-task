using System;
using JetBrains.Annotations;
using WebApp.Models;

namespace WebApp.Services.DataFiles
{
    public static class ExceptionExtensions
    {
        public static Exception AddData(this Exception ex, string key, object data)
        {
            ex.Data[key] = data;
            return ex;
        }

        public static Exception AddData(this Exception ex, [CanBeNull] Error[] errors)
        {
            if (errors == null)
                return ex;
            foreach (var error in errors)
            {
                ex.AddData(error.Code, error.Description);
            }

            return ex;
        }
    }
}