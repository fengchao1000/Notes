using Android.Content;
using Android.OS; 
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Notes.Droid.Renderers;
using System.ComponentModel; 
using Android.Text;
using Microsoft.AppCenter.Crashes;

[assembly: ExportRenderer(typeof(Notes.Controls.CustomItemLabel), typeof(CustomItemLabelRenderer))]
namespace Notes.Droid.Renderers
{
    public class CustomItemLabelRenderer : LabelRenderer
    {
        public CustomItemLabelRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var itemLabel = (Notes.Controls.CustomItemLabel)Element;
                var lineSpacing = itemLabel.LineSpacing;
                var maxLines = itemLabel.MaxLines;

                this.Control.SetLineSpacing(1f, (float)lineSpacing);
                if (maxLines > 1)
                {
                    this.Control.SetMaxLines(maxLines);
                    this.Control.Ellipsize = global::Android.Text.TextUtils.TruncateAt.End;
                }
                this.UpdateNativeControl();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Text")
            {
                this.UpdateNativeControl();
            }
        }
        private void UpdateNativeControl()
        {
            try
            { 
                if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
                {
                    this.Control.SetText(Html.FromHtml(Element.Text, FromHtmlOptions.ModeLegacy), Android.Widget.TextView.BufferType.Normal);
                }
                else
                {
                    this.Control.SetText(Html.FromHtml(Element.Text), Android.Widget.TextView.BufferType.Normal);
                }
            }
            catch (System.Exception ex)
            {
                this.Control.Text = Element.Text;
                Crashes.TrackError(ex);
            }
        }
    }
}