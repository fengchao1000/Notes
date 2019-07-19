using FC.Notes.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FC.Notes.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class NotesController : AbpController
    {
        protected NotesController()
        {
            LocalizationResource = typeof(NotesResource);
        }
    }
}