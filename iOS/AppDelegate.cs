using Foundation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using UIKit;
using TK.CustomMap.iOSUnified;
using Xamarin;
using Xamarin.Forms;
        

namespace FeatureDemo.Core.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            FormsMaps.Init();
            TKCustomMapRenderer.InitMapRenderer();
            NativePlacesApi.Init();
            LoadApplication(new App(new iOSInitializer()));



            return base.FinishedLaunching(app, options);
        }
    }

	public class iOSInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IUnityContainer container)
		{

		}
	}
}
