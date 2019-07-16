using Acr.UserDialogs;
using EuropeAesth.Custom;
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
	public partial class TemsilciDetail : ContentPage
	{
        AllUser secilenTemsilci;
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public TemsilciDetail (AllUser temsilci)
		{
            
			InitializeComponent ();
            secilenTemsilci = temsilci;
            BindingContext = this;
            TemsilciSil.Clicked += TemsilciSil_Clicked;
            if (App.Uyg.LoginUser.YetkiKod == 1)
            {
                TemsilciSil.IsVisible = true;
                bilgiDuzenle.IsVisible = true;
            }

            st_Temsilci.Children.Add(new HDLabel("Ad Soyad : ", temsilci.AdSoyad));
            st_Temsilci.Children.Add(new HDLabel("Email : ", temsilci.Email));
            st_Temsilci.Children.Add(new HDLabel("UserName : ", temsilci.UserKod));
            st_Temsilci.Children.Add(new HDLabel("Parola : ", temsilci.Parola));
            st_Temsilci.Children.Add(new HDLabel("Telefon : ", temsilci.Telefon));
            st_Temsilci.Children.Add(new HDLabel("Ulke : ", temsilci.Ulke));
            st_Temsilci.Children.Add(new HDLabel("Sehir : ", temsilci.Sehir));
        }

        private async void TemsilciSil_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Silme işlemi", "Bu temsilciyi silmek istiyor musunuz?", "Sil", "Vazgeç");
            if (result)
            {
                UserDialogs.Instance.ShowLoading("Siliniyor..", MaskType.Clear);

                try
                {
                    var AllUser = await firebase.Child("AllUser").OnceAsync<AllUser>();
                    var SlctUser = AllUser.FirstOrDefault(x => x.Object.UserKod == secilenTemsilci.UserKod).Key;
                    await firebase.Child("AllUser").Child(SlctUser).DeleteAsync();
                    await DisplayAlert("Silindi", "işlem başarılı", "Tamam");
                }
                catch (Exception)
                {
                    await DisplayAlert("Hata", "İşlem sırasında bir sıkıntı oldu. Temsilciyi kontrol edin", "Tamam");
                }
                UserDialogs.Instance.HideLoading();
                await Navigation.PopAsync();
            }
        }

    }
}