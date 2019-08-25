using Acr.UserDialogs;
using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Yonetici
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

            //CheckChange();
        }
        //private void CheckChange()
        //{
        //    firebase.Child("Yazilar")
        //        .AsObservable<KayitliHasta>()
        //        .Where(yazi => yazi.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
        //        .Subscribe(yazi =>
        //        {
        //            var rs = obsBekleyen.Any(x => x.KullaniciHasta.Id == yazi.Object.HastaId);
        //            if (!rs)//Insert
        //            {
        //                UserDialogs.Instance.ShowLoading("Yenileniyor", MaskType.None);
        //                yazi.Object.Id = yazi.Key;
        //                Obs_Yazi.Add(yazi.Object);
        //                Obs_Yazi = new ObservableCollection<YaziModel>(Obs_Yazi.OrderByDescending(x => x.Tarih).ToList());
        //            }
        //            else//update
        //            {
        //                var item = Obs_Yazi.Where(x => x.Id == yazi.Key).FirstOrDefault();
        //                var itemIndex = Obs_Yazi.IndexOf(item);
        //                if (yazi.Object.Tarih != item.Tarih)
        //                {
        //                    Obs_Yazi[itemIndex] = yazi.Object;
        //                    Obs_Yazi = new ObservableCollection<YaziModel>(Obs_Yazi.OrderByDescending(x => x.Tarih).ToList());
        //                }
        //                UserDialogs.Instance.ShowLoading("Yenileniyor", MaskType.None);

        //            }
        //            UserDialogs.Instance.HideLoading();
        //        });

        //    firebase.Child("Yazilar")
        //        .AsObservable<YaziModel>()
        //        .Where(yazi => yazi.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
        //        .Subscribe(async yazi =>
        //        {
        //            List<YaziModel> yazis = new List<YaziModel>();
        //            var tumYazilar = await firebase.Child("Yazilar").OnceAsync<YaziModel>();
        //            foreach (var item in tumYazilar)
        //                yazis.Add(item.Object);

        //            Obs_Yazi = new ObservableCollection<YaziModel>(yazis.OrderByDescending(x => x.Tarih).ToList());

        //        });

        //}
        private async void LstBekleyen_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var hasta = (Hasta)e.Item;
            await Navigation.PushAsync(new HastaDetail(hasta));

            LstBekleyen.SelectedItem = null;
        }
    }
}