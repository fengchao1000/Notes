using FormsToolkit;
using Notes.Helpers;
using Notes.Models;
using Notes.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Tasks
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskPage : ContentPage
    {
        private AddUpdateTaskViewModel viewModel;

        public AddTaskPage()
        {
            BindingContext = viewModel = new AddUpdateTaskViewModel(this);
        }

        /// <summary>
        /// 界面出现时执行
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeComponent();
        } 

        async void CancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}