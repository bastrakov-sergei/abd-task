using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp
{
    public class InvariantDoubleModelBinderProvider : IModelBinderProvider
    {
        [CanBeNull]
        public IModelBinder GetBinder([CanBeNull] ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (!context.Metadata.IsComplexType && (context.Metadata.ModelType == typeof(double) || context.Metadata.ModelType == typeof(double?)))
            {
                return new InvariantDoubleModelBinder(context.Metadata.ModelType);
            }

            return null;
        }
    }
}