using Acr.UserDialogs;
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
            HTelefon.TextChanged += HTelefon_TextChanged;
        }

        private void HTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //if ((e.OldTextValue == "" || e.OldTextValue == null) && e.NewTextValue == "0")
            //    HTelefon.Text = "";
            //if (HTelefon.Text.Length > 10)
            //    HTelefon.Text = e.OldTextValue;
        }

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Lütfen Bekleyiniz...", MaskType.None);
            FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

            var kullaniciHastalar = await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>();
            var KayitliHastalar = await firebase.Child("KayitliHasta").OnceAsync<KullaniciHasta>();

            bool kullaniciMi = false;
            bool kayitliMi = false;

            if (kullaniciHastalar != null && KayitliHastalar != null)
            {
                kullaniciMi = kullaniciHastalar.FirstOrDefault(x => x.Object.Telefon == HTelefon.Text).Object.Id;

                kayitliMi = KayitliHastalar.FirstOrDefault(x => x.Object.Id == HTelefon.Text).Object.Telefon; 


            }


            if (kullaniciMi && !kayitliMi)
            {
                var devamEt = await DisplayAlert("Bu Tlf No Kayıtlı", "Bu telefona ait başka bir hasta kayıtlı bulunuyor! Teklife devam etmek istemisiniz?", "Devam", "Vageç");
                if (devamEt)
                {
                    var hastaId = kullaniciHastalar.FirstOrDefault(x => x.Object.Telefon == HTelefon.Text).Object.Id;
                    await Navigation.PushModalAsync(new IslemPage(hastaId));

                }
                else
                    return;
            }

            if (kullaniciMi && kayitliMi)
            {
                await DisplayAlert("Kayitli","Bu numarada kayitli hasta var başka no deneyin","Tamam");
                return;
            }

            if (!kullaniciMi && !kayitliMi)
            {
                var HastaEkle = new KullaniciHasta()
                {
                    Id = Guid.NewGuid(),
                    AdSoyad = HAdSoyad.Text,
                    Email = HEmail.Text,
                    Telefon = HTelefon.Text,
                    Ulke = HUlke.Text,
                    YetkiKod = 3,
                    Şehir = HSehir.Text,
                    TemsilciKod = App.Uyg.LoginUser.UserKod,
                    
                };

                try
                {
                    await firebase.Child("KullaniciHastalar").PostAsync(HastaEkle);
                    await DisplayAlert("Başarılı", "Hasta başarılı bir şekilde Eklendi", "Tamam");
                    await Navigation.PushModalAsync(new IslemPage(HastaEkle.Id));

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
                }
            }

        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new TemsilciPage();
        }

        private void HTelefon_Focused(object sender, FocusEventArgs e)
        {
            if (HTelefon.Text == "" || HTelefon.Text == null)
            {
                HTelefon.Text = "+";

            }
        }
    }
}