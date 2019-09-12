using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Plugin.CurrentActivity;
using Plugin.GoogleClient;
using Android.Content;

namespace EuropeAesth.Droid
{
    [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);
            Acr.UserDialogs.UserDialogs.Init(this);
            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            GoogleClientManager.Initialize(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        private void InitControls()
        {
            CarouselViewRenderer.Init();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            GoogleClientManager.OnAuthCompleted(requestCode, resultCode, data);
        }

        public async override void OnBackPressed()
        {
            var result = await App.Current.MainPage.DisplayAlert("Çıkış", "Çıkmak mı istiyorsunuz?", "Evet", "Hayır");
            if (result)
            {
                base.OnBackPressed();
            }
        }
    }

}