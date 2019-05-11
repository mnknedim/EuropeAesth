using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.Pages
{
	public class TestPage : ContentPage
	{
		public TestPage ()
		{
            var btn = new Button() { Text = "Kayıt" };
            btn.Clicked += Btn_Clicked;
           
			Content = new StackLayout {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,

				Children = {
                    btn
                }
			};
		}

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        private async void  Btn_Clicked(object sender, EventArgs e)
        {
            var kayit = new AllUser()
            {
                UserKod = "ali.kilic",
                AdSoyad = "ALİ KILIÇ",
                Parola = "123456",
                Telefon = "5333042986",
                YetkiKod = 1,
                Email = "drkilicali@gmail.com",
                Sehir = "İSTANBUL",
                Ulke = "TÜRKİYE"
            };
            await firebase.Child("AllUser").PostAsync(kayit);
            return;

            var hotelekle = new MedicalIslem
            {
                Islem = "Saç Ekimi",
                Fiyat = 3500
            };

            var hotelekle2 = new MedicalIslem
            {
                Islem = "Sakal Ekimi",
                Fiyat = 2500
            };

            var hotelekle3 = new MedicalIslem
            {
                Islem = "Kaş Ekimi",
                Fiyat = 1500
            };

            await firebase.Child("MedicalIslemler").PostAsync(hotelekle);
            await firebase.Child("MedicalIslemler").PostAsync(hotelekle3);
            await firebase.Child("MedicalIslemler").PostAsync(hotelekle2);


        }
        
    }
}