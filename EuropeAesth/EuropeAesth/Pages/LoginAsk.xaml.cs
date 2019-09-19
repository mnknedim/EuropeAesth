using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Plugin.GoogleClient;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginAsk : PopupPage
	{
		public LoginAsk ()
		{
			InitializeComponent ();
            var tabGest = new TapGestureRecognizer();
            tabGest.Tapped += TabGest_Tapped;
            GoogleButton.GestureRecognizers.Add(tabGest);
		}

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        private async void TabGest_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await CrossGoogleClient.Current.LoginAsync();
                GoogleProfile Googleuser = new GoogleProfile
                {
                    Email = response.Data.Email,
                    FamilyName = response.Data.FamilyName,
                    GivenName = response.Data.GivenName,
                    Id = response.Data.Id,
                    Name = response.Data.Name,
                    Picture = response.Data.Picture
                };
                GUserCheckAndInsert(Googleuser);
                await SecureStorage.SetAsync("GoogleLogin", Googleuser.Email);
                MessagingCenter.Send<GoogleProfile>(Googleuser, "GoogleUser");
                await PopupNavigation.Instance.PopAsync();
                App.Current.MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.FromHex("#304f72") };
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        private async void GUserCheckAndInsert(GoogleProfile user = null)
        {
            var allGoogleUser = await firebase.Child("GoogleUsers").OnceAsync<GoogleProfile>();
            var IsThereGUser = allGoogleUser?.Any(x => x.Object.Email == user.Email);

            if (IsThereGUser == false)
            {
                await firebase.Child("GoogleUsers").PostAsync(user);
            }

            App.Uyg.GoogleGirisYapan = user;


        }

        private async void Giris_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BanaOzel());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}