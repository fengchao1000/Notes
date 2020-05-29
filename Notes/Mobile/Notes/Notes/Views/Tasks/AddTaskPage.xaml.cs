using FormsToolkit;
using Notes.Helpers;
using Notes.Models;
using Notes.ViewModels.Tasks;
using Notes.Views.Shared;
using Rg.Plugins.Popup.Services;
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
            await Navigation.PopAsync(); 
        }

        /// <summary>
        /// 点击显示 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnOpenStartDatePickerPopupPageTapped(object sender, System.EventArgs e)
        {
            var popupPage = new DatePickerPopupPage(MessageKeys.AddTaskStartDateKey);
            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        /// <summary>
        /// 点击显示 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnOpenEndDatePickerPopupPageTapped(object sender, System.EventArgs e)
        {
            var popupPage = new DatePickerPopupPage(MessageKeys.AddTaskEndDateKey);
            await PopupNavigation.Instance.PushAsync(popupPage);
        }
    }
}