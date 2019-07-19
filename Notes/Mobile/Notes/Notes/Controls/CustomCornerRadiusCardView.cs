using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Notes.Controls
{
    /// <summary>
    /// 圆角卡片视图
    /// </summary>
    public class CustomCornerRadiusCardView : Frame
    {
        public CustomCornerRadiusCardView()
        {
            HasShadow = false;
            //rderColor = (Color)Application.Current.Resources["CornerRadiusCardViewBorderColor"];
            BackgroundColor = (Color)Application.Current.Resources["CornerRadiusCardViewBackgroundColor"];

            if (Device.RuntimePlatform == Device.iOS)
            {
                CornerRadius = 8;
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                CornerRadius = 10;
            }
        }
    }

}
