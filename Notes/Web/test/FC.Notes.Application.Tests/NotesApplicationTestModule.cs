using Volo.Abp.Modularity;

namespace FC.Notes
{
    [DependsOn(
        typeof(NotesApplicationModule),
        typeof(NotesDomainTestModule)
        )]
    public class NotesApplicationTestModule : AbpModule
    {

    }
}