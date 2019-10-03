﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using Octane.Xamarin.Forms.VideoPlayer.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using UIKit;

namespace EuropeAesth.iOS
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
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(10,165,93);
            global::Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            CarouselViewRenderer.Init();
            Syncfusion.SfRating.XForms.iOS.SfRatingRenderer.Init();
            FormsVideoPlayer.Init();
            SfCalendarRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
        //public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        //{
        //    base.OpenUrl(app, url, options);
        //}


    }
}
