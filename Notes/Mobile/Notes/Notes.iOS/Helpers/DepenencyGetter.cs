using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Naxam.I18n;
using Naxam.I18n.Forms;
using Naxam.I18n.iOS;
using Notes.iOS.Helpers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DepenencyGetter))]
namespace Notes.iOS.Helpers
{
    public class DepenencyGetter : IDependencyGetter
    {
        readonly Dictionary<Type, object> cache;
        public DepenencyGetter()
        {
            ILocalizer localizer = new Localizer();
            cache = new Dictionary<Type, object> {
                {
                    typeof(ILocalizer),
                    localizer
                },
                {
                    typeof(ILocalizedResourceProvider),
                    new LocalizedResourceProvider(localizer, App.ResManager)
                }
            };
        }

        public T Get<T>()
        {
            return (T)cache[typeof(T)];
        }
    }
}