using EuropeAesth.Component;
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
	public partial class TemsilciPage : ContentPage
	{
        public static FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        public TemsilciPage ()
		{
			InitializeComponent ();

            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_kodUret.png", UnderText = "Kod Üret", TappedCommand = CallPages });
            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_kabulHastalar.png", UnderText = "Hastalar", PageName = new TaburcuEt(), TappedCommand = CallPages });
        }

        public Command CallPages = new Command(async (e) => {
            var Navigation = App.Current.MainPage.Navigation;
            var item = (ButtonView)e;

            if (item.PageName == null)
            {
                var AllKods = await firebase.Child("PromosyonKodlar").OnceAsync<PromosyonModel>();
                var _PromosyonKod = CreateKod(AllKods);

                var kodSonuc = await App.Current.MainPage.DisplayAlert("Promosyon Kod", $"{_PromosyonKod}","Ekle", "İptal");
                if (kodSonuc == true)
                {
                    try
                    {
                       var aa = await firebase.Child("PromosyonKodlar").PostAsync(new PromosyonModel
                        {
                            PromosyonKod = _PromosyonKod,
                            TemsilciKod = App.Uyg.TemsilciKod
                        });
                        await App.Current.MainPage.DisplayAlert("Başarılı", $"Kod : {_PromosyonKod}", "Tamam");

                    }
                    catch (Exception)
                    {

                        await App.Current.MainPage.DisplayAlert("Hata", "Kod Eklenmedi. Tekrar deneyin", "Tamam");
                    }
                }
                return;
            }

            await Navigation.PushModalAsync((item.PageName));
        });

        public static string CreateKod(IReadOnlyCollection<FirebaseObject<PromosyonModel>> allKods)
        {
            string pKod;
            Random random = new Random();
            pKod = string.Join(string.Empty, Enumerable.Range(0, 10).Select(number => random.Next(0, 9).ToString()));

            if (allKods.Any(x => x.Object.PromosyonKod == pKod))
                return CreateKod(allKods);

            return pKod;
        }
    }
}