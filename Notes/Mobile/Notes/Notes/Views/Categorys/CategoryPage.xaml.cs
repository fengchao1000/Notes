using Notes.Controls;
using Notes.Helpers;
using Notes.Models.Categorys;
using Notes.ViewModels.Categorys;
using Notes.Views.Bookmarks;
using System; 
using System.Threading.Tasks; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Categorys
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        private CategoryViewModel viewModel;

        private bool hasInitialize = false;

        public CategoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoryViewModel();
        }

        /// <summary>
        /// 界面出现时执行
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (!hasInitialize)
            {
                await viewModel.Initialize();

                foreach (Category item in viewModel.Categorys)
                {
                    CategoryContentView categoryContentView = new CategoryContentView(item);
                    this.CategoryStackLayout.Children.Add(categoryContentView);
                }

                hasInitialize = true;
            }
        }

        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnListViewItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var bookmarkPage = new BookmarkPage();
            await NavigationHelper.PushAsync(Navigation, bookmarkPage, false);
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PullToRefreshRefreshing(object sender, EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            viewModel.RefreshCommand.Execute(null);
            await Task.Delay(500);
            pullToRefresh.IsRefreshing = false;
        }

    }
}