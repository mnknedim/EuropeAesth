using EuropeAesth.Model;
using EuropeAesth.ViewPages;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BanaOzel : ContentPage
    {
        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        public BanaOzel ()
		{
			InitializeComponent ();
		}

        private async void GirisButon_Clicked(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Text == null || txtParola.Text == null)
            {
                await DisplayAlert("Giriş Kontrol", "Lütfen gerekli alanları doldurun", "Tamam");
                return;
            }

            if (txtKullaniciAdi.Text.Any(x => x == 'Y'))
            {
                var kullaniciResult = await firebase.Child("Yoneticiler").OnceAsync<YoneticiModel>();
                if (kullaniciResult != null)
                {
                    var kullaniciParola = kullaniciResult.Where(x => x.Object.YoneticiKod == txtKullaniciAdi.Text).FirstOrDefault().Object.Parola;
                    if (kullaniciParola == txtParola.Text)
                        await Navigation.PushModalAsync(new YoneticiPage());
                    else
                        await DisplayAlert("Hatalı Bilgi", "Lütfen bilgileirnizi kontrol edin", "Tamam");
                }
            }
            else if (txtKullaniciAdi.Text.Any(x => x == 'T'))
            {
                var kullaniciResult = await firebase.Child("Temsilciler").OnceAsync<TemsilciModel>();
                if (kullaniciResult != null)
                {
                    var kullanici = kullaniciResult.Where(x => x.Object.TemsilciKod == txtKullaniciAdi.Text).FirstOrDefault().Object;
                    if (kullanici.Parola == txtParola.Text)
                    {
                        await Navigation.PushModalAsync(new TemsilciPage());
                        App.Uyg.TemsilciKod = kullanici.TemsilciKod;
                    }
                    else
                        await DisplayAlert("Hatalı Bilgi", "Lütfen bilgileirnizi kontrol edin", "Tamam");
                }
            }
            else
            {
                var kullaniciResult = await firebase.Child("Kullanicilar").OnceAsync<KullaniciModel>();
                if (kullaniciResult != null)
                {
                    var kullaniciParola = kullaniciResult.Where(x => x.Object.Email == txtKullaniciAdi.Text).FirstOrDefault().Object.Parola;
                    if (kullaniciParola == txtParola.Text)
                        await Navigation.PushModalAsync(new UserPage());
                    else
                        await DisplayAlert("Hatalı Bilgi", "Lütfen bilgileirnizi kontrol edin", "Tamam");
                }
            }


        }

        private void Kayit_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegisterPage());
            //Navigation.PushModalAsync(new TestPage());
        }
    }
}