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
        public DatePickerPopupPage()
        {
            InitializeComponent();
        }
    }
}