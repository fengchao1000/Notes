using Notes.Controls;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FingerprintPage : ContentPage
    {
        public FingerprintPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面出现时执行
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnAuthenticat();
        }

        private async void OnAuthenticat()
        {
            var request = new AuthenticationRequestConfiguration("指纹识别", "请验证指纹用于登录")
            {
                CancelTitle = "取消"
            };

            var result = await CrossFingerprint.Current.AuthenticateAsync(request);

            var authenticationType = await CrossFingerprint.Current.GetAuthenticationTypeAsync();//返回认证类型

            var fingerprintAvailability = await CrossFingerprint.Current.GetAvailabilityAsync(true);//返回当前手机上面可用的指纹

            var isAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);//是否有可用的指纹

            await SetResultAsync(result);
        }

        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
            {
                App.Current.MainPage = new CustomNavigationPage(new MainPage());//跳转页面   
            }
            else
            {
                 
            }
        }
    }
}