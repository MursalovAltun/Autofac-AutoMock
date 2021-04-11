using System;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebAPI;

namespace UnitTests.Components
{
    public class TestFixture
    {
        public ApplicationContext Context { get; set; }
        public IMapper Mapper { get; }

        public TestFixture(Action<IMapperConfigurationExpression> configure = null)
        {
            Context = TestApplicationContextFactory.Create();

            if (configure == null) return;

            var configurationProvider = new MapperConfiguration(configure);

            Mapper = configurationProvider.CreateMapper();
        }

        public Action<ContainerBuilder> BeforeBuild => cfg =>
        {
            cfg.RegisterInstance(Context).As<IApplicationContext>();

            if (Mapper != null)
            {
                cfg.RegisterInstance(Mapper).As<IMapper>();
            }
        };

        public TestFixture RegisterUserManager()
        {
            var services = new ServiceCollection();
            services.AddScoped<IApplicationContext>(_ => Context);
            services.AddLogging();

            return this;
        }
    }
}