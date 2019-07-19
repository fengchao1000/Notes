using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FC.Notes.Data;
using Volo.Abp.DependencyInjection;

namespace FC.Notes.EntityFrameworkCore
{
    [Dependency(ReplaceServices = true)]
    public class EntityFrameworkCoreNotesDbSchemaMigrator 
        : INotesDbSchemaMigrator, ITransientDependency
    {
        private readonly NotesMigrationsDbContext _dbContext;

        public EntityFrameworkCoreNotesDbSchemaMigrator(NotesMigrationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}