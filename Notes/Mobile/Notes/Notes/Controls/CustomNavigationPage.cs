using Notes.Helpers; 
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Controls
{ 
    /// <summary>
    /// 自定义导航栏
    /// </summary>
    public class CustomNavigationPage : NavigationPage
    {
        // 首次按下返回键时间戳
        private DateTime firstBackPressedTime = DateTime.MinValue;
        public CustomNavigationPage(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            Icon = root.Icon;
        }

        public CustomNavigationPage()
        {
            Init();
        }

        void Init()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                BarBackgroundColor = (Color)Application.Current.Resources["NavigationText"];
            }

            BarBackgroundColor = (Color)Application.Current.Resources["NavigationText"];
            BarTextColor = Color.White; 
        }

        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.Android && this.RootPage == this.CurrentPage)
            {
                if (firstBackPressedTime == DateTime.MinValue || firstBackPressedTime.AddSeconds(3) < DateTime.Now)
                {
                    ToastHelper.Current.SendToast("再按一次退出程序");
                    firstBackPressedTime = DateTime.Now;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return base.OnBackButtonPressed();
        }
    }
}
