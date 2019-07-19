using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace FC.Notes.Web
{
    [Dependency(ReplaceServices = true)]
    public class NotesBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Notes";
    }
}
