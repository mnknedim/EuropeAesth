using EuropeAesth.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EuropeAesth
{
    public partial class App : Application
    {
        public static App Uyg => Current as App;

        public string TemsilciKod;
        public string GirenMail;
        public App()
        {
            InitializeComponent();

            MainPage = new TabbedMainPage();
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
