using Naxam.I18n;
using Naxam.I18n.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Notes.Helpers
{
    public class ResourcesHelper
    {
        public static ResourcesHelper Current { get; set; } = new ResourcesHelper();

        public string this[string key]
            => Translate(key);
         
        /// <summary>
        /// Get a translation from a key, formatting the string with the given params, if any
        /// </summary>
        public string Translate(string key, params object[] args)
        {
            var getter = DependencyService.Get<IDependencyGetter>();
            var localizer = getter.Get<ILocalizer>();
            var localizeResProvider = getter.Get<ILocalizedResourceProvider>();
            return localizeResProvider.GetText(key);
        }

        public void SetLocale(string localeType)
        {
            var getter = DependencyService.Get<IDependencyGetter>();
            var localizer = getter.Get<ILocalizer>(); 
            var culture = new CultureInfo(localeType);
            localizer.SetLocale(culture); 
        } 

    }
}
