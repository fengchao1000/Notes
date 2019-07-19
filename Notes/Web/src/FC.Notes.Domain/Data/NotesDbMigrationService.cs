using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace FC.Notes.Data
{
    public class NotesDbMigrationService : ITransientDependency
    {
        public ILogger<NotesDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly INotesDbSchemaMigrator _dbSchemaMigrator;

        public NotesDbMigrationService(
            IDataSeeder dataSeeder,
            INotesDbSchemaMigrator dbSchemaMigrator)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrator = dbSchemaMigrator;

            Logger = NullLogger<NotesDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            Logger.LogInformation("Migrating database schema...");
            await _dbSchemaMigrator.MigrateAsync();

            Logger.LogInformation("Executing database seed...");
            await _dataSeeder.SeedAsync();

            Logger.LogInformation("Successfully completed database migrations.");
        }
    }
}