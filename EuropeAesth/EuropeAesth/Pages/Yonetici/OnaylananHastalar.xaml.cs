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
	public partial class OnaylananHastalar : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        ObservableCollection<Hasta> obsOnaylananlar = new ObservableCollection<Hasta>();
		public OnaylananHastalar(IEnumerable<FirebaseObject<KayitliHasta>> onaylananHastalar)
		{
			InitializeComponent ();
            Load(onaylananHastalar);
		}

        private async void Load(IEnumerable<FirebaseObject<KayitliHasta>> onaylananHastalar)
        {
            var allKullaniciHasta = await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>();
            foreach (var item in onaylananHastalar)
            {
                var hasta = allKullaniciHasta.FirstOrDefault(x=>x.Object.Id == item.Object.HastaId).Object;
                
                obsOnaylananlar.Add(new Hasta {KayitliHasta = item.Object, KullaniciHasta = hasta });
            }

            LstOnaylanan.BindingContext = obsOnaylananlar;
        }

        private async void LstOnaylanan_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var hasta = (Hasta)e.Item;
           await Navigation.PushModalAsync(new HastaDetail(hasta));
        }
    }
}