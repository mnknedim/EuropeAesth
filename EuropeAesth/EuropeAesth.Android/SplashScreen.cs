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

namespace EuropeAesth.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, Label = "EuropeAesth", Icon = "@drawable/logoandroid")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Finish();

        }
    }
}