using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using Syncfusion.Licensing;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EuropeAesth
{
    public partial class App : Application
    {
        public static App Uyg => Current as App;

        public TemsilciModel LoginTemsilci;
        public AllUser LoginUser;
        public string GirenMail;
        public App()
        {
            InitializeComponent();
            SyncfusionLicenseProvider.RegisterLicense("OTQwNjVAMzEzNzJlMzEyZTMwV0c2ODdyQU1jeDBuTUozM3lVZE1uNnpQTEI0Rkc3WGJxRU5RMXBwOHczND0=");
            MainPage = new NavigationPage(new MainPage() ) { BarTextColor = Color.FromHex("#304f72") };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
