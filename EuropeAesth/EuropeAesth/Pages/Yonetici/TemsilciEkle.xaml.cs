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
        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");

        public TemsilciEkle ()
		{
            
			InitializeComponent ();
		}

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            var Temsilci = new TemsilciModel()
            {
                TemsilciKod = FTemsilciKod.Text,
                YetkiKod = 2,
                AdSoyad = FTAdSoyad.Text,
                Parola = FParola.Text,
                TemsilciAd = FTemsilciAd.Text,
                Telefon = FTelefon.Text,
                Sehir = FSehir.Text,
                Ulke = FUlke.Text,
            };

            try
            {
                await firebase.Child("Temsilciler").PostAsync(Temsilci);
                await DisplayAlert("", "Eklendi", "Tamam");
                
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