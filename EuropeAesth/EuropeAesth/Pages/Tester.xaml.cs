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
	public partial class Tester : ContentPage
	{
		public Tester ()
		{
			InitializeComponent ();

            
        }
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        private async void Button_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("ekleniyor",MaskType.Gradient);
            var EklenecekVideo = new VideoModel()
            {
                Baslik = "Saç Ekimi",
                Aciklama = "Saç Ekimi hakkında",
                ImageUrl = "asd",
                VideoUrl = "asdasd",
                Tarih = DateTime.Now
            };

            try
            {

                await firebase.Child("Videolar").PostAsync(EklenecekVideo);
                await DisplayAlert("Kaydedildi", "Kayıt Başarılı", "Tamam");
                await Navigation.PopModalAsync();
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. Tekrar Deneyiniz. {ex.Message}", "Tamam");
            }
        }
    }
}