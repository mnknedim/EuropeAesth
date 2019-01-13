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

            var kayit = new KullaniciModel
            {
                Email = txtEmail.Text,
                AdSoyad = txtAdSoyad.Text,
                Parola = txtParola.Text,
                Telefon = txtTelefon.Text,
                Ulke = txtUlke.Text,
                YetkiKod = 3,
                Şehir = txtSehir.Text,
                PromosyonKod = txtPromosyonKod.Text
            };

            try
            {
                await firebase.Child("Kullanicilar").PostAsync(kayit);
                await DisplayAlert("", "Eklendi", "Tamam");
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
            }
        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

      
    }
}