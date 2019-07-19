using Notes.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Helpers
{
    public class ToastHelper
    {
        static IToastHelper toastHelper;
        public static IToastHelper Current => toastHelper ?? (toastHelper = DependencyService.Get<IToastHelper>());
    }
}
