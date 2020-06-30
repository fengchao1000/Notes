using NotesShellDemo.Helper;
using NotesShellDemo.Views.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesShellDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnBookmarkClicked(object sender, EventArgs e)
        { 
             await NavigationHelper.PushAsync(Navigation, new TestTabbedPage(), false);
        }

        private void OnTestClicked(object sender, EventArgs e)
        {

        }
    }
}