﻿using EuropeAesth.Model;
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
            var tumHastalar = await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>();
            var kayitliVarmi = tumHastalar.Any(x => x.Object.Telefon == HTelefon.Text);
            if (kayitliVarmi)
            {
                var devamEt = await DisplayAlert("Bu Tlf No Kayıtlı", "Bu telefona ait başka bir hasta kayıtlı bulunuyor! Teklife devam etmek istemisiniz?", "Devam","Vageç");
                if (devamEt)
                    await Navigation.PushModalAsync(new IslemPage(HTelefon.Text));
                else
                    return;
            }

            var HastaEkle = new KullaniciHasta()
            {
                AdSoyad = HAdSoyad.Text,
                Email = HEmail.Text,
                Telefon = HTelefon.Text,
                Ulke = HUlke.Text,
                YetkiKod = 3,
                Şehir = HSehir.Text,
                TemsilciKod = App.Uyg.TemsilciKod
            };

            try
            {
                await firebase.Child("KullaniciHastalar").PostAsync(HastaEkle);
                await DisplayAlert("Başarılı", "Hasta başarılı bir şekilde Eklendi", "Tamam");
                await Navigation.PushModalAsync(new IslemPage(HTelefon.Text));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
            }
        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new TemsilciPage();
        }
    }
}