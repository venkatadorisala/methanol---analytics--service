using System;
using System.Collections.Generic;
using System.Text;
using JM.Integration.Methanol.Services.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JM.Integration.Methanol.DB.Models;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

[assembly: WebJobsStartup(typeof(DbInitializationService), "DbMigration")]
namespace JM.Integration.Methanol.Services.Config
{
    public class DbInitializationService : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<DbSeedConfigProvider>();
        }

        internal class DbSeedConfigProvider : IExtensionConfigProvider
        {
            private readonly IServiceScopeFactory _scopeFactory;

            public DbSeedConfigProvider(IServiceScopeFactory scopeFactory)
            {
                _scopeFactory = scopeFactory;
            }

            public void Initialize(ExtensionConfigContext context)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<LevoMethanolDBContext>();

               //dbContext.Database.
                var migrator = dbContext.Database.GetService<IMigrator>();
                //Migrate the changes.
                migrator.Migrate();
               
            }
        }
    }
}
