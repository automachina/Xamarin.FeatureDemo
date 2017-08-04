using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FeatureDemo.Core.Droid.Helpers;
using FeatureDemo.Core.Controls;

[assembly: ExportRenderer(typeof(FlatButton), typeof(FlatButtonRenderer))]
namespace FeatureDemo.Core.Droid.Helpers
{
    public class FlatButtonRenderer : ButtonRenderer
    {
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }
    }
}
