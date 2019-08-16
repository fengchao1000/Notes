using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Naxam.I18n;
using Naxam.I18n.Forms;
using Xamarin.Forms;

namespace Notes.Views
{
    public class MyPage : ContentPage
    {
        public MyPage()
        {
            Title = "Demo I18N";

            var getter = DependencyService.Get<IDependencyGetter>();

            var localizer = getter.Get<ILocalizer>();
            var localizeResProvider = getter.Get<ILocalizedResourceProvider>();

            var defaultCulture = localizer.GetCurrentCultureInfo();
            var viCulture = new CultureInfo("zh-cn");

            var button = new Button
            {
                Text = localizeResProvider.GetText("Login"),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var isVi = false;
            button.Clicked += (s, e) => {
                var culture = !isVi ? viCulture : defaultCulture;

                localizer.SetLocale(culture);

                isVi = !isVi;

                button.Text = localizeResProvider.GetText("Login");

                Navigation.PushAsync(new HomePage());
            };

            Content = button;
        }
    }
}
