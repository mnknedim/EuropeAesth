using Acr.UserDialogs;
using EuropeAesth.Custom;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HastaDetail : ContentPage
	{
        public string TemsilciName
        {
            get { return (string)GetValue(TemsilciNameProperty); }
            set { SetValue(TemsilciNameProperty, value); }
        }
        public static readonly BindableProperty TemsilciNameProperty = BindableProperty.Create("TemsilciName", typeof(string), typeof(HastaDetail), default(string));

        Hasta _hasta;
        HDLabel temsilci;
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public HastaDetail (Hasta _Hasta)
		{
			InitializeComponent ();
            if (App.Uyg.LoginUser.YetkiKod == 1)
            {
                HastaSil.IsVisible = true;

                if (_Hasta.KayitliHasta.OnayDurumu == 0)
                    Onayla.IsVisible = true;

                if (_Hasta.KayitliHasta.OnayDurumu == 1)
                    TaburcuEt.IsVisible = true;

            }

            LoadTemsilci();

            _hasta = _Hasta;
            st_Hasta.Children.Add(new HDLabel ("Ad Soyad : ", _Hasta.KullaniciHasta.AdSoyad));
            st_Hasta.Children.Add(new HDLabel ("Email : ", _Hasta.KullaniciHasta.Email));
            st_Hasta.Children.Add(new HDLabel ("Telefon : ", _Hasta.KullaniciHasta.Telefon));
            st_Hasta.Children.Add(new HDLabel ("Ülke : " , _Hasta.KullaniciHasta.Ulke));
            st_Hasta.Children.Add(new HDLabel ("Şehir : " , _Hasta.KullaniciHasta.Şehir));
            

            var KHasta = _Hasta.KayitliHasta;
            st_HastaIslem.Children.Add(new HDLabel ( "İşlem : " , KHasta.Islem));
            st_HastaIslem.Children.Add(new HDLabel ( "Hotel : " , KHasta.Hotel));
            st_HastaIslem.Children.Add(new HDLabel ( "Son Durum : " , KHasta.SonDurum));
            st_HastaIslem.Children.Add(new HDLabel ( "GirisTarih : " , KHasta.GirisTarih.ToString().Substring(0, KHasta.GirisTarih.ToString().IndexOf(" "))));
            st_HastaIslem.Children.Add(new HDLabel (  "CikisTarih : " , KHasta.CikisTarih.ToString().Substring(0, KHasta.CikisTarih.ToString().IndexOf(" "))));
            st_HastaIslem.Children.Add(new HDLabel ( "Kaç Gün : " , KHasta.GunSayisi.ToString()));
            st_HastaIslem.Children.Add(new HDLabel ( "Transfer : " , KHasta.Transfer ));
            st_HastaIslem.Children.Add(new HDLabel ( "Toplam ₺ : " , KHasta.ToplamFiyatTl + " ₺"));
            st_HastaIslem.Children.Add(new HDLabel ("Toplam € : " , KHasta.ToplamFiyatEuro +  " €"));
            st_HastaIslem.Children.Add(new HDLabel ("Teklif € : " , KHasta.VerilenTeklifEuro + "€"));


        }

        private async void LoadTemsilci()
        {
            var temsilciler = await firebase.Child("AllUser").OnceAsync<AllUser>();
            TemsilciName = temsilciler.FirstOrDefault(x => x.Object.UserKod == _hasta.KullaniciHasta.TemsilciKod).Object.AdSoyad;
        }


        private async void Onayla_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Onaylanıyor..",MaskType.Clear);
            var hasta = (await firebase.Child("KayitliHasta").OnceAsync<KayitliHasta>()).FirstOrDefault(x => x.Object.HastaId == _hasta.KayitliHasta.HastaId);
            hasta.Object.OnayDurumu = 1;
            await firebase.Child("KayitliHasta").Child(hasta.Key).PutAsync(hasta.Object);
            UserDialogs.Instance.HideLoading();
            await DisplayAlert("Onaylandı", "Hasta onaylandı", "Tamam");
            App.Current.MainPage = new YoneticiPage();
        }

        private async void TaburcuEt_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Taburcu ediliyor..", MaskType.Clear);
            var hasta = (await firebase.Child("KayitliHasta").OnceAsync<KayitliHasta>()).FirstOrDefault(x => x.Object.HastaId == _hasta.KayitliHasta.HastaId);
            hasta.Object.OnayDurumu = 2;
            await firebase.Child("KayitliHasta").Child(hasta.Key).PutAsync(hasta.Object);
            UserDialogs.Instance.HideLoading();
            await DisplayAlert("Taburcu", "Hasta taburcu edildi", "Tamam");
            App.Current.MainPage = new YoneticiPage();
        }

        private async void HastaSil_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Taburcu ediliyor..", MaskType.Clear);
            var Kayitlihasta = (await firebase.Child("KayitliHasta").OnceAsync<KayitliHasta>()).FirstOrDefault(x => x.Object.HastaId == _hasta.KayitliHasta.HastaId);
            var Kullancihasta = (await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>()).FirstOrDefault(x => x.Object.Id == _hasta.KullaniciHasta.Id);
            
            await firebase.Child("KayitliHasta").Child(Kayitlihasta.Key).DeleteAsync();
            await firebase.Child("KullaniciHastalar").Child(Kullancihasta.Key).DeleteAsync();
            UserDialogs.Instance.HideLoading();
            await DisplayAlert("Silme", "Hasta silindi", "Tamam");
            App.Current.MainPage = new YoneticiPage();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TemsilciNameProperty.PropertyName)
            {
                st_Hasta.Children.Add(new HDLabel("Temsilci : ", TemsilciName));
            }
        }

        private async void BtnGeri_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}