using FC.Notes.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace FC.Notes.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(NotesEntityFrameworkCoreDbMigrationsModule),
        typeof(NotesApplicationContractsModule)
        )]
    public class NotesDbMigratorModule : AbpModule
    {
        
    }
}
