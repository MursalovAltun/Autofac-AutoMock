using Autofac;
using Autofac.AttributeExtensions;

namespace WebAPI
{
    public class AutoRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAttributedClasses(ThisAssembly);
        }
    }
}