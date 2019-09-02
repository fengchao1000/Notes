using Naxam.I18n;
using Naxam.I18n.Forms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();



            var items = new List<DateTime>();

            for (int i = 0; i < 100; i++)
            {
                items.Add(DateTime.Now.AddMinutes(-i * (5 + i % 13)));
            }

            lstDates.ItemsSource = items;

        }


        async void OnButtonClicked(object sender, System.EventArgs e)
        {
            var getter = DependencyService.Get<IDependencyGetter>();
            var localizer = getter.Get<ILocalizer>();

            var viCultureENUS = new CultureInfo("en-us");

            var culture = viCultureENUS;

            var data = localizer.GetCurrentCultureInfo();
             
            localizer.SetLocale(culture);

            base.OnAppearing();

            Navigation.PushAsync(new TestPage()); 

        }
    }
}