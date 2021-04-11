using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPI.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifeTime;

        public MigrationHostedService(
            IServiceProvider serviceProvider,
            IHostApplicationLifetime hostApplicationLifeTime)
        {
            _serviceProvider = serviceProvider;
            _hostApplicationLifeTime = hostApplicationLifeTime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartDatabaseMigrationAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task StartDatabaseMigrationAsync(CancellationToken cancellationToken)
        {
            var context = _serviceProvider.GetRequiredService<ApplicationContext>();

            await context.Database.MigrateAsync(cancellationToken);
        }
    }
}