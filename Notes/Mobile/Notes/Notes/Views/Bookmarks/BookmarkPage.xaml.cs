using Notes.Helpers;
using Notes.Models.Bookmarks;
using Notes.Models.Categorys;
using Notes.ViewModels.Bookmarks;
using Notes.Views.Article;
using Notes.Views.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Bookmarks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookmarkPage : ContentPage
    {
        private BookmarkViewModel viewModel;

        private bool hasInitialize = false;

        public BookmarkPage(Category category)
        {
            InitializeComponent();

            BindingContext = viewModel = new BookmarkViewModel(category); 
        }

        public BookmarkPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new BookmarkViewModel(new Category());
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

                var prevPage = Navigation.NavigationStack.FirstOrDefault(p => p is CategoryPage);
                if (prevPage == null)
                {
                    Navigation.InsertPageBefore(new CategoryPage(), this);
                }
            }
        }

        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnListViewItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Bookmark bookmark = e.ItemData as Bookmark;
            //var articlesDetailsPage = new ArticlesDetailsPage(new Models.Articles.ArticlesDto { Url = bookmark.LinkUrl,Title= bookmark.Title});
            //await NavigationHelper.PushAsync(Navigation, articlesDetailsPage, false);

            var bookmarkDetailPage = new BookmarkDetailPage(bookmark);
            await NavigationHelper.PushAsync(Navigation, bookmarkDetailPage, false); 
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