﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;
using FFImageLoading;

namespace MonkeyChat.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            global::FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            ImageService.Instance.Initialize();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
            Xamarin.Calabash.Start();
#endif
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
