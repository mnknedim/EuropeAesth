using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Temsilci
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BekleyenHastalar : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        ObservableCollection<Hasta> obsBekleyen = new ObservableCollection<Hasta>();
        public BekleyenHastalar (IEnumerable<FirebaseObject<KayitliHasta>> bekleyenHastalar)
		{
			InitializeComponent ();
            Load(bekleyenHastalar);
		}

        private async void Load(IEnumerable<FirebaseObject<KayitliHasta>> bekleyenHastalar)
        {
            var allKullaniciHasta = await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>();
            foreach (var item in bekleyenHastalar)
            {
                var hasta = allKullaniciHasta.FirstOrDefault(x => x.Object.Id == item.Object.HastaId).Object;
                obsBekleyen.Add(new Hasta { KayitliHasta = item.Object, KullaniciHasta = hasta });
            }

            LstBekleyen.BindingContext = obsBekleyen;
        }

        private async void LstBekleyen_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var hasta = (Hasta)e.Item;
            await Navigation.PushModalAsync(new HastaDetail(hasta));
        }
    }
}