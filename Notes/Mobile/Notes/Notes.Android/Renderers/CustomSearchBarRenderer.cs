using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Notes.Controls;
using Notes.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Icu.Text.DateFormat;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace Notes.Droid.Renderers
{
    [Obsolete]
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        public CustomSearchBarRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //去掉下划线
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.Transparent);

                //修改光标颜色
                var textViewId = Resources.GetIdentifier("android:id/search_src_text", null, null);
                var textView = Control.FindViewById(textViewId); 
                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                JNIEnv.SetField(textView.Handle, mCursorDrawableResProperty, Resource.Drawable.color_cursor);
                 
                //改变搜索图标
                //var searchView = (Control as SearchView);
                //var searchIconId = searchView.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                //if (searchIconId > 0)
                //{
                //    var searchPlateIcon = searchView.FindViewById(searchIconId);
                //    (searchPlateIcon as ImageView).SetColorFilter(color.ToAndroid(), PorterDuff.Mode.SrcIn);
                //}
            }
        }
    }
}