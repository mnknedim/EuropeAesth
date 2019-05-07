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
        ObservableCollection<KullaniciHasta> obsBekleyen = new ObservableCollection<KullaniciHasta>();
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
                obsBekleyen.Add(hasta);
            }

            LstBekleyen.BindingContext = obsBekleyen;
        }

        private void LstBekleyen_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}