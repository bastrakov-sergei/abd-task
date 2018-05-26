using System;
using Hangfire;
using JetBrains.Annotations;

namespace WebApp.Hangfire
{
    public class ContainerJobActivator : JobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public ContainerJobActivator(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

        public override object ActivateJob([NotNull] Type type) => serviceProvider.GetService(type);
    }
}
