using Notes.ViewModels.Categorys;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        private CategoryViewModel viewModel;

        private bool hasInitialize = false;

        public TaskListPage()
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
    }
}