using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Notes.Helpers
{
    public class SizeHelper
    {
        public class PIXEL : Dictionary<string, double>
        {
            public new double this[string key]
            {
                get
                {
                    return Convert.ToDouble(key) * SizeHelper.flag;
                }
                set
                {

                }
            }
        }
        public class MARGIN : Dictionary<string, Thickness>
        {
            public new Thickness this[string key]
            {
                get
                {
                    string[] p = key.Split('-');
                    if (p.Length == 1)
                        return new Thickness(Convert.ToDouble(p[0]) * SizeHelper.flag);
                    return new Thickness(Convert.ToDouble(p[0]) * SizeHelper.flag,
                        Convert.ToDouble(p[1]) * SizeHelper.flag,
                        Convert.ToDouble(p[2]) * SizeHelper.flag,
                        Convert.ToDouble(p[3]) * SizeHelper.flag);
                }
                set
                {

                }
            }
        }

        public static double flag;
        public PIXEL px
        {
            get;
        }
        public MARGIN m
        {
            get;
        }
        public SizeHelper()
        {　 //计算出屏幕缩放比例，640是你的UI原始设计图的高度
            flag = 1;//App.ScreenWidth / 720.0;
            px = new PIXEL();
            m = new MARGIN();
        }
    }

}
