using Notes.ViewModels.Shared;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerPopupPage : PopupPage
    {
        private DatePickerPopupViewModel viewModel;

        public DatePickerPopupPage(string messageKey)
        {
            InitializeComponent();

            BindingContext = viewModel = new DatePickerPopupViewModel(messageKey);
        }

        private void DatepickerDateSelected(object sender, Syncfusion.XForms.Pickers.DateChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                viewModel.NewDate = (DateTime)e.OldValue;
            }
            else
            {
                viewModel.NewDate = (DateTime)e.NewValue;
            } 
        }
    }
}