using Notes.Helpers;
using Notes.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        private TaskListViewModel viewModel;

        private bool hasInitialize = false;

        public TaskListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TaskListViewModel();
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
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PullToRefreshRefreshing(object sender, EventArgs e)
        {
            pullToRefresh.IsRefreshing = true;
            viewModel.RefreshCommand.Execute(null);
            await Task.Delay(1000);
            pullToRefresh.IsRefreshing = false;
        }

        async void AddTaskClicked(object sender, EventArgs e)
        {
            await NavigationHelper.PushAsync(Navigation, new AddTaskPage(), false);
        }
    }
}