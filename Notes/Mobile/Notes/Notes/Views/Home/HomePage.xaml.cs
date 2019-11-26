using Notes.Helpers;
using Notes.Views.Bookmarks;
using Notes.Views.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击书签按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnBookmarkClicked(object sender, System.EventArgs e)
        {
            await NavigationHelper.PushAsync(Navigation, new CategoryTestPage(), false);
        }

        /// <summary>
        /// 点击书签按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnTestClicked(object sender, System.EventArgs e)
        {
            await NavigationHelper.PushAsync(Navigation, new BookmarkPage(), false);
        }
    } 
}