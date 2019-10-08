using Acr.UserDialogs;
using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Linq;
using EuropeAesth.VM;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BanaOzel : ContentPage
    {
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public BanaOzel() 
		{
			InitializeComponent ();
            UserDialogs.Instance.HideLoading();
        }


        private void GirisButon_Clicked(object sender, EventArgs e)
        {
            Giris(UserName.Text, Password.Text);
        }

        private async void Giris(string userN, string pass)
        {
            try
            {
                if (UserName.Text == null || Password.Text == null)
                {
                    await DisplayAlert("Giriş Kontrol", "Lütfen gerekli alanları doldurun", "Tamam");
                    return;
                }
                UserDialogs.Instance.ShowLoading("Lütfen Bekleyiniz..", MaskType.Black);

                var userResult = await firebase.Child("AllUser").OnceAsync<AllUser>();
                if (userResult != null)
                {
                    var user = userResult.FirstOrDefault(x => x.Object.UserKod == userN && x.Object.Parola == pass).Object;
                    if (user == null)
                    {
                        await DisplayAlert("Hatalı Giriş", "Lütfen bilgileri kontrol edin", "Tamam");
                        return;
                    }

                    App.Uyg.LoginUser = user;

                    if (user.YetkiKod == 1)
                    {
                        await Navigation.PushAsync(new YoneticiPage());
                        try
                        {
                            await SecureStorage.SetAsync("UserKod", userN);
                            await SecureStorage.SetAsync("Parola", pass);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }

                    if (user.YetkiKod == 2)
                    {
                        await Navigation.PushAsync(new TemsilciPage());

                        try
                        {
                            await SecureStorage.SetAsync("UserKod", userN);
                            await SecureStorage.SetAsync("Parola", pass);
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                        
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Hata", "Lütfen bilgileirnizi kontrol edin ve tekrar deneyin", "Tamam");
            }

            UserDialogs.Instance.HideLoading();

          App.Current.MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.FromHex("#304f72") };

        }

        private void Kayit_Tapped(object sender, EventArgs e)
        {
           
        }
    }
}