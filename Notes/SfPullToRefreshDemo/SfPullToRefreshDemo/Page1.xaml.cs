using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SfPullToRefreshDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {


        public Page1()
        {
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //pullToRefresh.IsRefreshing = true;
            //await Task.Delay(1000);
            //pullToRefresh.IsRefreshing = true;

            //Device.BeginInvokeOnMainThread(() => {
            //    pullToRefresh.StartRefreshing();
            //});
            //await Task.Delay(3000);
            //pullToRefresh.EndRefreshing();


            Device.BeginInvokeOnMainThread(() => {
                pullToRefresh.IsRefreshing = true;
            });
            await Task.Delay(3000);
            pullToRefresh.IsRefreshing = false;

        }

        private async void OnPullToRefreshRefreshing(object sender, EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(1000);
            pullToRefresh.IsRefreshing = false;
        }
    }
     
}