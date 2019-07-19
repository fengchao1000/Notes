using Android.Widget;
using Notes.Interfaces;
using PA.Mobile.Droid.Helpers;
using Xamarin.Forms; 

[assembly: Xamarin.Forms.Dependency(typeof(ToasterAndroidHelper))]
namespace PA.Mobile.Droid.Helpers
{
    public class ToasterAndroidHelper : IToastHelper
    {
        public void SendToast(string message)
        {
            //var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    Toast.MakeText(context, message, ToastLength.Long).Show();
            //});
        }
    }
}