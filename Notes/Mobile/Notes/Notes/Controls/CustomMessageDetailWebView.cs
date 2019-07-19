using Notes.Helpers;
using System;
using Xam.Plugin.WebView.Abstractions;
using Xamarin.Forms;

namespace Notes.Controls
{
    /// <summary>
    /// 展示消息详情的WebView
    /// </summary>
    public class CustomMessageDetailWebView : FormsWebView
    {
        public static readonly BindableProperty LoadStatusProperty = BindableProperty.Create(nameof(LoadStatus), typeof(LoadMoreStatus), typeof(CustomMessageDetailWebView), LoadMoreStatus.StausDefault, propertyChanged: OnLoadStatusChanged);

        public LoadMoreStatus LoadStatus
        {
            get { return (LoadMoreStatus)GetValue(LoadStatusProperty); }
            set { SetValue(LoadStatusProperty, value); }
        }
        static void OnLoadStatusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as CustomMessageDetailWebView).NotifyLoadStatus((LoadMoreStatus)newValue);
        }
        public async void NotifyLoadStatus(LoadMoreStatus loadStatus)
        {
            await this.InjectJavascriptAsync("updateLoadStatus(" + (int)loadStatus + ");");
        }
    }
}
