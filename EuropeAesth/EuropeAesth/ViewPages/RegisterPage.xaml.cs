using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.ViewPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");

        public RegisterPage ()
		{
			InitializeComponent ();
		}

        private async void KayitOl_Clicked(object sender, EventArgs e)
        {
            var promosyonKods = await firebase.Child("PromosyonKodlar").OnceAsync<PromosyonModel>();
            var proKodVar = promosyonKods.Any(x => x.Object.PromosyonKod == txtPromosyonKod.Text);

            if (txtPromosyonKod.Text == null || !proKodVar)
            {
                await DisplayAlert("Promosyon Hata", "Hatalı Promosyon Kodu", "Tamam");
                return;
            }

            var kayit = new KullaniciModel
            {
                Email = txtEmail.Text,
                AdSoyad = txtAdSoyad.Text,
                Parola = txtParola.Text,
                Telefon = txtTelefon.Text,
                Ulke = txtUlke.Text,
                YetkiKod = 3,
                Şehir = txtSehir.Text,
                PromosyonKod = txtPromosyonKod.Text,
                HastaKabul = false,
            };

            try
            {
                await firebase.Child("Kullanicilar").PostAsync(kayit);
                await DisplayAlert("Kayıt", "Kayıt Başarılı.Giriş sayfasına yönelendirileceksiniz", "Tamam");
                await Navigation.PopModalAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Hata", "Bilinmedik bir hata oluştu.Lütfen tesmilcinize danışın", "Tamam");
            }
        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

      
    }
}