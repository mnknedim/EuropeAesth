using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Temsilci
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IslemPage : ContentPage
	{
        public string Total
        {
            get { return (string)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }
        public static readonly BindableProperty TotalProperty = BindableProperty.Create("Total", typeof(string), typeof(IslemPage), default(string));

        public ObservableCollection<HotelModel> Oteller
        {
            get { return (ObservableCollection<HotelModel>)GetValue(OtellerProperty); }
            set { SetValue(OtellerProperty, value); }
        }
        public static readonly BindableProperty OtellerProperty = BindableProperty.Create("Oteller", typeof(ObservableCollection<HotelModel>),
            typeof(IslemPage), default(ObservableCollection<HotelModel>));

        public ObservableCollection<MedicalIslem> Islemler
        {
            get { return (ObservableCollection<MedicalIslem>)GetValue(IslemlerProperty); }
            set { SetValue(IslemlerProperty, value); }
        }
        public static readonly BindableProperty IslemlerProperty = BindableProperty.Create("Islemler", typeof(ObservableCollection<MedicalIslem>),
            typeof(IslemPage), default(ObservableCollection<MedicalIslem>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");

        List<MedicalIslem> ListIslem = new List<MedicalIslem>();
        List<HotelModel> ListOteller = new List<HotelModel>();

        public IslemPage ()
		{
            BindingContext = this;
			InitializeComponent ();
            Load();
        }

        private async void Load()
        {

            var temsilciResult = await firebase.Child("Oteller").OnceAsync<HotelModel>();
            foreach (var item in temsilciResult)
                ListOteller.Add(item.Object);

            var islemResult = await firebase.Child("MedicalIslemler").OnceAsync<MedicalIslem>();
            foreach (var item in islemResult)
                ListIslem.Add(item.Object);

            Oteller = new ObservableCollection<HotelModel>(ListOteller);
            Islemler = new ObservableCollection<MedicalIslem>(ListIslem);

            IslemP.SelectedIndexChanged += IslemP_SelectedIndexChanged;
            HotelP.SelectedIndexChanged += HotelP_SelectedIndexChanged;
            Transfer.SelectedIndexChanged += Transfer_SelectedIndexChanged;
        }

        private void Transfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var transferSender = (((Picker)sender).SelectedItem as string);
            var transfer = transferSender == "Var" ? 400 : 0;
            var otel = HotelP.SelectedItem != null ? HotelP.SelectedItem as HotelModel : new HotelModel { Fiyat = 0 };
            var islem = IslemP.SelectedItem != null ? IslemP.SelectedItem as MedicalIslem : new MedicalIslem { Fiyat = 0 };
            Total = (otel.Fiyat + islem.Fiyat + transfer).ToString() + " ₺";

        }

        private void HotelP_SelectedIndexChanged(object sender, EventArgs e)
        {
            GunSayisi.IsEnabled = true;
            var hotelSender = ((Picker)sender).SelectedItem as HotelModel;
            var islem = IslemP.SelectedItem != null ? IslemP.SelectedItem as MedicalIslem : new MedicalIslem { Fiyat = 0 };
            var transfer = (string)Transfer.SelectedItem == "Var" ? 400 : 0;
            Total = (hotelSender.Fiyat + islem.Fiyat + transfer).ToString() + " ₺";
        }

        private void IslemP_SelectedIndexChanged(object sender, EventArgs e)
        {

            var islemSender =((Picker)sender).SelectedItem as MedicalIslem;
            var otel = HotelP.SelectedItem != null ? HotelP.SelectedItem as HotelModel : new HotelModel { Fiyat = 0};
            var transfer = (string)Transfer.SelectedItem == "Var" ? 400 : 0;
            Total = (islemSender.Fiyat + otel.Fiyat + transfer).ToString() + " ₺";
        }

        private void DevamButon_Clicked(object sender, EventArgs e)
        {
            var islem = IslemP.SelectedItem.ToString();
            var hotel = HotelP.SelectedItem.ToString();
            var transfer = Transfer.SelectedItem.ToString();
            
        }

        private void GunSayisi_TextChanged(object sender, TextChangedEventArgs e)
        {
            var otelFiyat = (HotelP.SelectedItem as HotelModel).Fiyat;
            var GunBirlikte = GunSayisi.Text == "" ? otelFiyat * 1 : Convert.ToInt32(GunSayisi.Text) * otelFiyat;
            var islem = IslemP.SelectedItem != null ? IslemP.SelectedItem as MedicalIslem : new MedicalIslem { Fiyat = 0 };
            var transfer = (string)Transfer.SelectedItem == "Var" ? 400 : 0;
            Total = (GunBirlikte + islem.Fiyat + transfer).ToString() + " ₺";
        }
    }
}