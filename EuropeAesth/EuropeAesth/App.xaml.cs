using Acr.UserDialogs;
using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using Firebase.Database;
using Syncfusion.Licensing;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EuropeAesth
{
    public partial class App : Application
    {
        public static App Uyg => Current as App;

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public TemsilciModel LoginTemsilci;
        public AllUser LoginUser;
        public MainPageMenuItem PageMenuItem;
        public string GirenMail;
        public App()
        {
            InitializeComponent();
            SyncfusionLicenseProvider.RegisterLicense("OTQwNjVAMzEzNzJlMzEyZTMwV0c2ODdyQU1jeDBuTUozM3lVZE1uNnpQTEI0Rkc3WGJxRU5RMXBwOHczND0=");
            MainPage = new NavigationPage(new MainPage() ) { BarTextColor = Color.FromHex("#304f72") };
        }

        protected async override void OnStart()
        {
            await CheckLogined();
        }

        private async Task CheckLogined()
        {
           
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
