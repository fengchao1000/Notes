using FormsToolkit;
using Notes.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModels.Shared
{
    public class DatePickerPopupViewModel : BaseViewModel
    {
        public DateTime NewDate { get; set; } = DateTime.Now.Date;
        public string MessageKey { get; set; }

        public DatePickerPopupViewModel(string messageKey) 
        {
            MessageKey = messageKey;
        }

        public async Task Cancel()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async Task OK()
        {
            if (NewDate != DateTimeHelper.DefaultDateTime)
            {
                MessagingService.Current.SendMessage(MessageKey, NewDate); 
            }

            await PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        /// 取消
        /// </summary>  
        public ICommand CancelCommand => new Command(async () => await Cancel());

        /// <summary>
        /// 确定
        /// </summary>  
        public ICommand OkCommand => new Command(async () => await OK());
    }
}
