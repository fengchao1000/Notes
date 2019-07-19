using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace FC.Notes.Data
{
    /* This is used if database provider does't define
     * INotesDbSchemaMigrator implementation.
     */
    public class NullNotesDbSchemaMigrator : INotesDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}