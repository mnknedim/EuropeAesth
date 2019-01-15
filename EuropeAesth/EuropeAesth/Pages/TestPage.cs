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

        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        private async void  Btn_Clicked(object sender, EventArgs e)
        {
            var kayit = new HastahaneModel()
            {
                KısaAd = "İstanbul Estetic",
                HastahaneAd = "Adjuvan Clinic (Zincirlikuyu)",
                Adres = "Esentepe Mahallesi, Keskin Kalem Sk. No:1, 34394 Şişli/İstanbul",
                Telefon = "(0212) 211 34 31",
                Logation = "41.069188, 29.006477",
                HastahaneKod = "ZK34",
            };
           
            await firebase.Child("Hastahaneler").PostAsync(kayit);
           
            
        }
        
    }
}