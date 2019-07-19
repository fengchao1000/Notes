using FC.Notes.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FC.Notes.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class NotesPageModel : AbpPageModel
    {
        protected NotesPageModel()
        {
            LocalizationResourceType = typeof(NotesResource);
        }
    }
}