using Notes.Helpers;
using Notes.ViewModels.Categorys;
using Notes.Views.Bookmarks;
using Notes.Views.Categorys;
using Notes.Views.Tasks;
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
        private CategoryViewModel viewModel;

        private bool hasInitialize = false;

        public HomePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoryViewModel();
        }

        /// <summary>
        /// 界面出现时执行
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!hasInitialize)
            {
                viewModel.Initialize();

                hasInitialize = true;
            }
        }

        /// <summary>
        /// 点击书签按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnBookmarkClicked(object sender, System.EventArgs e)
        {
            await NavigationHelper.PushAsync(Navigation, new CategoryPage(), false);
        }

        /// <summary>
        /// 点击书签按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnTestClicked(object sender, System.EventArgs e)
        {
            await NavigationHelper.PushAsync(Navigation, new AddTaskPage(), false);
        }
    } 
}