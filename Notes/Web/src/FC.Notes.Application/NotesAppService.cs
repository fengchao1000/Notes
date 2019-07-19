using System;
using System.Collections.Generic;
using System.Text;
using FC.Notes.Localization;
using Volo.Abp.Application.Services;

namespace FC.Notes
{
    /* Inherit your application services from this class.
     */
    public abstract class NotesAppService : ApplicationService
    {
        protected NotesAppService()
        {
            LocalizationResource = typeof(NotesResource);
        }
    }
}
