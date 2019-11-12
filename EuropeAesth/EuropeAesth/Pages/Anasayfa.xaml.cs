using Acr.UserDialogs;
using EuropeAesth.Model;
using EuropeAesth.Pages.GoogleUser;
using Firebase.Database;
using Rg.Plugins.Popup.Extensions;
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
	public partial class Anasayfa : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

        public double CaroselHeight
        {
            get => (double)GetValue(CaroselHeightProperty);
            set => SetValue(CaroselHeightProperty, value);
        }
        public static readonly BindableProperty CaroselHeightProperty =
            BindableProperty.Create(nameof(CaroselHeight), typeof(double), typeof(Anasayfa), default(double));

        DisplayInfo displayInfo;
        public Anasayfa ()
		{
            InitializeComponent ();
            BindingContext = this;
            displayInfo = DeviceDisplay.MainDisplayInfo;
            CaroselHeight = displayInfo.Height / 5.2;
            if (displayInfo.Density <= 1.2)
            {
                CaroselHeight = displayInfo.Height / 3;
            }

        }

        private void UserLogin_Clicked(object sender, EventArgs e)
        {
            CheckLogin();
        }

        private async void CheckLogin()
        {
            try
            {
                var userName = await SecureStorage.GetAsync("UserKod");
                var passWord = await SecureStorage.GetAsync("Parola");
                var gLogin = await SecureStorage.GetAsync("GoogleLogin");

                if (userName == null)
                {
                    if (gLogin != null)
                    {
                        var gUserResponse = await firebase.Child("GoogleUsers").OnceAsync<Model.GoogleUser>();
                        var googleProfile = gUserResponse.FirstOrDefault(x => x.Object.Email == gLogin)?.Object;
                        await Navigation.PushAsync(new GUserPage(googleProfile));
                        
                    }
                    else
                        await Navigation.PushPopupAsync(new LoginAsk("Anasayfa"), true);
                }

                var userResult = await firebase.Child("AllUser").OnceAsync<AllUser>();
                if (userResult != null)
                {
                    var user = userResult.FirstOrDefault(x => x.Object.UserKod == userName && x.Object.Parola == passWord).Object;
                    if (user == null)
                    {
                        await Navigation.PushPopupAsync(new LoginAsk());
                    }
                    else
                    {
                        App.Uyg.LoginUser = user;
                        
                        if (user.YetkiKod == 1)
                        {
                            await Navigation.PushAsync(new YoneticiPage());
                        }

                        else if (user.YetkiKod == 2)
                        {
                            await Navigation.PushAsync(new TemsilciPage());
                            
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

        }
    }
}