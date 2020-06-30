using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NotesShellDemo.Views.Chart
{ 
   public class TestTabbedPage : TabbedPage
    {
        bool hasInitialization;

        public TestTabbedPage()
        {
            Title = "图表";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!hasInitialization)
            {
                this.Children.Add(new ChartTestPage() { Title = "测试1" });
                this.Children.Add(new ChartTestPage() { Title = "测试2" });
                this.Children.Add(new ChartTestPage() { Title = "测试3" });
                this.Children.Add(new ChartTestPage() { Title = "测试4" });
                this.Children.Add(new ChartTestPage() { Title = "测试5" });

                hasInitialization = true;
            }

        }

        /// <summary>
        /// 界面消失
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }


   
}
