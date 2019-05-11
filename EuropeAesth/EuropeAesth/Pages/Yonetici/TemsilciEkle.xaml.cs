using Acr.UserDialogs;
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

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TemsilciEkle : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

        public TemsilciEkle ()
		{
            
			InitializeComponent ();
		}

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Kayıt ediliyor", MaskType.Gradient);
            var kayitKontrol = await firebase.Child("AllUser").OnceAsync<AllUser>();
            if (kayitKontrol == null)
                return;
            else
            {
                if (kayitKontrol.Any(x=>x.Object.UserKod == TemsilciKod.Text))
                {
                    await DisplayAlert("Kayıt Başarısız","Bu Temsilci kodunda kayıt bulunuyor. Farklı deneyiniz","");
                    return;
                }
            }

            var Temsilci = new AllUser()
            {
                UserKod = TemsilciKod.Text,
                YetkiKod = 2,
                Email = Email.Text,
                AdSoyad = AdSoyad.Text,
                Parola = Parola.Text,
                Telefon = Telefon.Text,
                Sehir = Sehir.Text,
                Ulke = Ulke.Text,
            };

            try
            {
                await firebase.Child("AllUser").PostAsync(Temsilci);
                await DisplayAlert("Kayıt", "Başarılı", "Tamam");
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
            }
            UserDialogs.Instance.HideLoading();

        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}