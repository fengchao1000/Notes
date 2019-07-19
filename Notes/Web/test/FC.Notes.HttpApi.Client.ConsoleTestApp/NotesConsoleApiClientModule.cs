using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace FC.Notes.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(NotesHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class NotesConsoleApiClientModule : AbpModule
    {
        
    }
}
