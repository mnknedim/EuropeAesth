using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using Plugin.CurrentActivity;
//using Plugin.GoogleClient;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using EuropeAesth.Droid.Renderer;
using EuropeAesth.Renderer;
using Xamarin.Forms;

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
            //GoogleClientManager.Initialize(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            DependencyService.Register<IGoogleManager, GoogleManager>();
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

        public override async void OnBackPressed()
        {
            var result = await App.Current.MainPage.DisplayAlert("Çıkış", "Çıkmak mı istiyorsunuz?", "Evet", "Hayır");
            if (result)
            {
                base.OnBackPressed();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                GoogleManager.Instance.OnAuthCompleted(result);
            }
        }
    }

}