using FC.Notes.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FC.Notes
{
    [DependsOn(
        typeof(NotesEntityFrameworkCoreTestModule)
        )]
    public class NotesDomainTestModule : AbpModule
    {

    }
}