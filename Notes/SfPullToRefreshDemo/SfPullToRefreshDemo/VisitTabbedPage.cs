using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SfPullToRefreshDemo
{
    public class VisitTabbedPage : TabbedPage
    {
        public VisitTabbedPage()
        {
            this.Children.Add(new Page1() { Title = "Page1" });
            this.Children.Add(new Page1() { Title = "Page2" });
            this.Children.Add(new Page1() { Title = "Page3" });
        }
    }
}
