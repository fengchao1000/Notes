using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfPullToRefreshDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpendTab(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new VisitTabbedPage(),false); 
        }

        private async  void OnOpendPage(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Page1(), false);
        }
    }
}
