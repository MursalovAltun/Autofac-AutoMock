using System;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace UnitTests.Components
{
    public class TestApplicationContextFactory
    {
        public static TestApplicationContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies()
                .Options;

            var context = new TestApplicationContext(options);

            context.Database.EnsureCreated();

            context.SaveChanges();

            return context;
        }
    }

    public class TestApplicationContext : ApplicationContext
    {
        public override void Dispose()
        {
            Database.EnsureDeleted();
            base.Dispose();
        }

        public TestApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}