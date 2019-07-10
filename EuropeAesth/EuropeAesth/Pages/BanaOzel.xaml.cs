﻿using Acr.UserDialogs;
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
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public BanaOzel ()
		{
			InitializeComponent ();
		}

        private async void GirisButon_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new TestPage());
            //return;
            try
            {
                if (UserName.Text == null || Password.Text == null)
                {
                    await DisplayAlert("Giriş Kontrol", "Lütfen gerekli alanları doldurun", "Tamam");
                    return;
                }

                UserDialogs.Instance.ShowLoading("Lütfen Bekleyiniz..", MaskType.Black);

                var userResult = await firebase.Child("AllUser").OnceAsync<AllUser>();
                if (userResult != null)
                {
                    var user = userResult.FirstOrDefault(x => x.Object.UserKod == UserName.Text && x.Object.Parola == Password.Text).Object;
                    if (user == null)
                    {
                        await DisplayAlert("Hatalı Giriş", "Lütfen bilgileri kontrol edin", "Tamam");
                        return;
                    }

                    App.Uyg.LoginUser = user;

                    if (user.YetkiKod == 1)
                        await Navigation.PushAsync(new YoneticiPage());

                    if (user.YetkiKod == 2)
                        await Navigation.PushAsync(new TemsilciPage());

                }
                    

                //if (UserName.Text.Any(x => x == 'Y'))
                //{
                //    var kullaniciResult = await firebase.Child("Yoneticiler").OnceAsync<YoneticiModel>();
                //    if (kullaniciResult != null)
                //    {
                //        var kullaniciParola = kullaniciResult.Where(x => x.Object.YoneticiKod == UserName.Text).FirstOrDefault().Object.Parola;
                //        if (kullaniciParola == Password.Text)
                //            await Navigation.PushModalAsync(new YoneticiPage());
                //        else
                //            await DisplayAlert("Hatalı Bilgi", "Lütfen bilgileirnizi kontrol edin", "Tamam");
                //    }

                //}
                //else if (UserName.Text.Any(x => x == 'T'))
                //{
                //    var kullaniciResult = await firebase.Child("Temsilciler").OnceAsync<TemsilciModel>();
                //    if (kullaniciResult != null)
                //    {
                //        var kullanici = kullaniciResult.Where(x => x.Object.TemsilciKod == UserName.Text).FirstOrDefault().Object;
                //        if (kullanici.Parola == Password.Text)
                //        {
                //            await Navigation.PushModalAsync(new TemsilciPage());
                //            App.Uyg.LoginTemsilci = kullanici;
                //        }
                //        else
                //            await DisplayAlert("Hatalı Bilgi", "Lütfen bilgileirnizi kontrol edin", "Tamam");
                //    }
                //}
                //else
                //{
                //    var kullaniciResult = await firebase.Child("Kullanicilar").OnceAsync<KullaniciModel>();
                //    if (kullaniciResult != null)
                //    {
                //        var kullaniciParola = kullaniciResult.Where(x => x.Object.Email == UserName.Text).FirstOrDefault().Object.Parola;
                //        if (kullaniciParola == Password.Text)
                //            await Navigation.PushModalAsync(new UserPage());
                //        else
                //            await DisplayAlert("Hatalı Bilgi", "Lütfen bilgileirnizi kontrol edin", "Tamam");
                //    }
                //}
            }
            catch (Exception)
            {

                await DisplayAlert("Hata", "Lütfen bilgileirnizi kontrol edin ve tekrar deneyin", "Tamam");
            }


            UserDialogs.Instance.HideLoading();
        }

        private void Kayit_Tapped(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new RegisterPage());
            Navigation.PushModalAsync(new TestPage());
        }
    }
}