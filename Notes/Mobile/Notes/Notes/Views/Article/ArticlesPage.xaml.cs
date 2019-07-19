using Notes.Helpers;
using Notes.Models.Articles;
using Notes.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Article
{
    [XamlCompilation(XamlCompilationOptions.Compile)] 
    public partial class ArticlesPage : ContentPage
    {
        ArticlesViewModel viewModel;

        public ArticlesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ArticlesViewModel();

            viewModel.LoadMessagePageList();
        }

        /// <summary>
        /// 界面出现时执行
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadMessagePageList();
        }

         
        #region 事件

        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnListViewItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            ArticlesDto baseMessageEntity = e.ItemData as ArticlesDto;
            var messagDetailPage = new ArticlesDetailsPage(baseMessageEntity);
            //await NavigationHelper.PushAsync(Navigation, messagDetailPage, false);

            await Navigation.PushAsync(messagDetailPage);
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PullToRefreshRefreshing(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 滑动结束，在Item上面进行滑动，获取到滑动的ItemIndex，后续可以根据ItemIndex进行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewSwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            viewModel.ItemIndex = e.ItemIndex;
        }

        /// <summary>
        /// 滑动开始，在Item上面进行滑动，获取到滑动的ItemIndex，后续可以根据ItemIndex进行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewSwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            viewModel.ItemIndex = -1;
        }

        #endregion

    }
}