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

        private bool hasInitialize = false;

        public AddTaskPage()
        {
           
        }

        /// <summary>
        /// 界面出现时执行
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing(); 

            if (!hasInitialize)
            {
                InitializeComponent();
                 
                BindingContext = viewModel = new AddUpdateTaskViewModel(this);

                hasInitialize = true;
            } 
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
        private void OnOpenTaskPriorityPickerTapped(object sender, System.EventArgs e)
        {
            this.pickerTaskPriority.IsOpen = true;
        }

        /// <summary>
        /// 点击显示 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnOpenTaskTypePickerTapped(object sender, System.EventArgs e)
        {
            this.pickerTaskType.IsOpen = true;
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

        private void PickerTaskPriorityCancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            
        }

        private void PickerTaskPriorityOkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            this.entryTaskPriority.Text = e.NewValue.ToString(); 

            if (this.entryTaskPriority.Text == TaskPriority.Lower.ToString())
            {
                this.viewModel.TaskModel.Priority = TaskPriority.Lower;
            }

            if (this.entryTaskPriority.Text == TaskPriority.Ordinary.ToString())
            {
                this.viewModel.TaskModel.Priority = TaskPriority.Ordinary;
            }

            if (this.entryTaskPriority.Text == TaskPriority.emergency.ToString())
            {
                this.viewModel.TaskModel.Priority = TaskPriority.emergency;
            }

            if (this.entryTaskPriority.Text == TaskPriority.VeryUrgent.ToString())
            {
                this.viewModel.TaskModel.Priority = TaskPriority.VeryUrgent;
            }
        }

        private void PickerTaskTypeCancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {

        }

        private void PickerTaskTypeOkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            this.entryTaskType.Text = e.NewValue.ToString(); 

            if (this.entryTaskType.Text == TaskType.Day.ToString()) 
            {
                this.viewModel.TaskModel.TaskType = TaskType.Day;
            }

            if (this.entryTaskType.Text == TaskType.Year.ToString())
            {
                this.viewModel.TaskModel.TaskType = TaskType.Year;
            }

            if (this.entryTaskType.Text == TaskType.Month.ToString())
            {
                this.viewModel.TaskModel.TaskType = TaskType.Month;
            }
        }
    }
}