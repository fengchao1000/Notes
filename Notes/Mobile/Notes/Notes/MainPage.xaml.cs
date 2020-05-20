using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            //底部TabBar背景色
            this.BarBackgroundColor = (Color)Xamarin.Forms.Application.Current.Resources["BottomTabBarBackgroundColor"];//Color.White;  
            this.BarTextColor = Color.Black;
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title; 
        }
    }
}