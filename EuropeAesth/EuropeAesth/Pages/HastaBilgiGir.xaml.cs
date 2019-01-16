using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HastaBilgiGir : ContentPage
	{
        public KullaniciModel Kullanici
        {
            get => (KullaniciModel)GetValue(KullaniciProperty);
            set => SetValue(KullaniciProperty, value);
        }
        public static readonly BindableProperty KullaniciProperty = BindableProperty.Create("Kullanici", typeof(KullaniciModel), typeof(HastaBilgiGir), default(KullaniciModel));

        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        ObservableCollection<HastahaneModel> HastahaneList = new ObservableCollection<HastahaneModel>();
        ObservableCollection<HotelModel> OtelList = new ObservableCollection<HotelModel>();


		public HastaBilgiGir ()
		{
            BindingContext = this;
			InitializeComponent ();
            Load();

        }
        public async void Load()
        {
            var hastahaneler =await firebase.Child("Hastahaneler").OnceAsync<HastahaneModel>();
            foreach (var item in hastahaneler)
                HastahaneList.Add(item.Object);

            HastahanePicker.ItemsSource = HastahaneList;

            var oteller = await firebase.Child("Oteller").OnceAsync<HotelModel>();
            foreach (var item in oteller)
                OtelList.Add(item.Object);

            OtelPicker.ItemsSource = OtelList;
        }

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            var hastane = HastahanePicker.SelectedItem as HastahaneModel;
            var hotel = OtelPicker.SelectedItem as HotelModel;
            var YeniHasta = new HastaModel()
            {
                HastahaneKod = hastane.HastahaneKod,
                KullaniciMail = Kullanici.Email,
                HotelKod = hotel.HotelKod,
                TransferKod = "C340102",
            };
            await firebase.Child("Hastalar").PostAsync(YeniHasta);

            Kullanici.HastaKabul = true;

            var kull = await firebase.Child("Kullanicilar").PatchAsync
            
                
        
        }
    }
}