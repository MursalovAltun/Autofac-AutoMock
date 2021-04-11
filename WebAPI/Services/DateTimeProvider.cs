using System;
using Autofac.AttributeExtensions;
using WebAPI.Services.Abstraction;

namespace WebAPI.Services
{
    [InstancePerLifetimeScope(AsImplementedInterfaces = true)]
    public class UtcNowProvider : IUtcNowProvider
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}