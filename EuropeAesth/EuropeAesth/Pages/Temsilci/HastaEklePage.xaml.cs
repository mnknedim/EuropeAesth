using EuropeAesth.Model;
using EuropeAesth.Pages.Temsilci;
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
    public partial class HastaEklePage : ContentPage
    {
        public HastaEklePage()
        {
            InitializeComponent();
        }

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");

            var HastaEkle = new Hasta()
            {
                AdSoyad = HAdSoyad.Text,
                Email = HEmail.Text,
                Telefon = HTelefon.Text,
                Ulke = HUlke.Text,
                YetkiKod = 3,
                Şehir = HSehir.Text,
                TemsilciKod = "TGM01"
            };

            try
            {
                await firebase.Child("Hastalar").PostAsync(HastaEkle);
                await DisplayAlert("", "Eklendi", "Tamam");
                await Navigation.PushModalAsync(new IslemPage());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
            }
        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {

        }
    }
}