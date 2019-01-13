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
            var kayit = new YoneticiModel()
            {
                AdSoyad = "Ali Kılıç",
                YoneticiAd = "Adjuvan",
                Parola = "123456",
                Telefon = "05426377112",
                YetkiKod = 1,
                YoneticiKod = "340102"
            };

            await firebase.Child("Yoneticiler").PostAsync(kayit);
           
            
        }
        
    }
}