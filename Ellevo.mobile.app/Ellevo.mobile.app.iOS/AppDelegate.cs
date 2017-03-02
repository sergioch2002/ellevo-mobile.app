using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Foundation;
using UIKit;
using Ellevo.mobile.app;

namespace Ellevo.mobile.app.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // Newer version of Xamarin Studio and Visual Studio provide the
            // ENABLE_TEST_CLOUD compiler directive in the Debug configuration,
            // but not the Release configuration.
//#if ENABLE_TEST_CLOUD
//Xamarin.Calabash.Start();
//#endif
            global::Xamarin.Forms.Forms.Init();
            MobileCenter.Configure("e4e9727d-f4cf-4d3e-acc2-e2f08bec4fb5");
            LoadApplication(new Ellevo.mobile.app.App());

            

            return base.FinishedLaunching(app, options);
        }
    }
}
