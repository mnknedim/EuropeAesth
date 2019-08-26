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

namespace EuropeAesth.Pages.Yonetici
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TaburcuHastalar : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        ObservableCollection<KullaniciHasta> obsTaburcular= new ObservableCollection<KullaniciHasta>();
        ObservableCollection<KayitliHasta> _taburcuKayitlilar;
        public TaburcuHastalar (IEnumerable<FirebaseObject<KayitliHasta>> taburcuHastalar)
		{
			InitializeComponent ();
            Load(taburcuHastalar);
		}

        private async void Load(IEnumerable<FirebaseObject<KayitliHasta>> taburcuHastalar)
        {
            _taburcuKayitlilar = new ObservableCollection<KayitliHasta>();
            var allKullaniciHasta = await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>();
            foreach (var item in taburcuHastalar)
            {
                _taburcuKayitlilar.Add(item.Object);
                var hasta = allKullaniciHasta.FirstOrDefault(x => x.Object.Id == item.Object.HastaId).Object;
                obsTaburcular.Add(hasta);
            }

            LstTaburcu.BindingContext = obsTaburcular;
        }

        private async void LstTaburcu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var hastaKul = (KullaniciHasta)e.Item;

            var hasta = new Hasta { KullaniciHasta = hastaKul, KayitliHasta = _taburcuKayitlilar.Where(x => x.HastaId == hastaKul.Id).FirstOrDefault() };
            await Navigation.PushAsync(new HastaDetail(hasta));
            LstTaburcu.SelectedItem = null;
        }
    }
}