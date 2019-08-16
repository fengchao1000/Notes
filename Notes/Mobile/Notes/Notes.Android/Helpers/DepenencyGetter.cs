using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Naxam.I18n;
using Naxam.I18n.Droid;
using Naxam.I18n.Forms;
using Notes.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(DepenencyGetter))]
namespace Notes.Droid.Helpers
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