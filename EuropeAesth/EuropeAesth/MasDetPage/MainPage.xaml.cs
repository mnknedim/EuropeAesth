using Acr.UserDialogs;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.MasDetPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Page page;

            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            if (item.Title == "Profilim")
                page = await CheckLogin();

            else
                page = (Page)Activator.CreateInstance(item.TargetType);
           
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        private async Task<Page> CheckLogin()
        {
            Page page = null;
            try
            {
                var userName = await SecureStorage.GetAsync("UserKod");
                var passWord = await SecureStorage.GetAsync("Parola");

                if (userName == null)
                {
                    UserDialogs.Instance.HideLoading();
                    page = new BanaOzel();

                }

                var userResult = await firebase.Child("AllUser").OnceAsync<AllUser>();
                if (userResult != null)
                {
                    var user = userResult.FirstOrDefault(x => x.Object.UserKod == userName && x.Object.Parola == passWord).Object;
                    if (user == null)
                    {
                        UserDialogs.Instance.HideLoading();
                        page = new BanaOzel();
                    }

                    App.Uyg.LoginUser = user;

                    if (user.YetkiKod == 1)
                        page = new YoneticiPage();

                    if (user.YetkiKod == 2)
                        page = new TemsilciPage();

                }
                UserDialogs.Instance.HideLoading();

            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return page;
        }
    }
}