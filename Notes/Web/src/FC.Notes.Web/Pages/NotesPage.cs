using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using FC.Notes.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FC.Notes.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits FC.Notes.Web.Pages.NotesPage
     */
    public abstract class NotesPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<NotesResource> L { get; set; }
    }
}
