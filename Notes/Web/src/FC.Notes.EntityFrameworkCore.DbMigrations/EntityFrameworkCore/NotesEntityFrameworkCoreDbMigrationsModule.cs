using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace FC.Notes.EntityFrameworkCore
{
    [DependsOn(
        typeof(NotesEntityFrameworkCoreModule)
        )]
    public class NotesEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NotesMigrationsDbContext>();
        }
    }
}
