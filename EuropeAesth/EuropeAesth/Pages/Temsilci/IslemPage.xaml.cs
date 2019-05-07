using EuropeAesth.Custom;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

        public string TotalEuro
        {
            get { return (string)GetValue(TotalEuroProperty); }
            set { SetValue(TotalEuroProperty, value); }
        }
        public static readonly BindableProperty TotalEuroProperty = BindableProperty.Create("TotalEuro", typeof(string), typeof(IslemPage), default(string));

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

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

        List<MedicalIslem> ListIslem = new List<MedicalIslem>();
        List<HotelModel> ListOteller = new List<HotelModel>();
        DateTime GirisTime;
        DateTime CikisTime;
        
        string secTarih = "";
        string HastaTelKod;

        public IslemPage (string hastatel)
		{
            HastaTelKod = hastatel;
            BindingContext = this;
			InitializeComponent ();
            GirisTarih.Focused += GirisTarih_Focused;
            CikisTarih.Focused += CikisTarih_Focused;
            calendar.SelectionChanged += Calendar_SelectionChanged;
            Load();
        }

        private void CikisTarih_Focused(object sender, FocusEventArgs e)
        {
            secTarih = "Çıkış";
            CalenderGrid.IsVisible = true;
            var aa = calendar.SelectedDate;
        }

        private void GirisTarih_Focused(object sender, FocusEventArgs e)
        {
            secTarih = "Giriş";
            CalenderGrid.IsVisible = true;
            var aa = calendar.SelectedDate;
        }

        int days = 1;
        private void Calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            
            var sfcalender = sender as SfCalendar;
            var dateString = sfcalender.SelectedDate.Value.ToString();
            var showDate = dateString.Substring(0, dateString.IndexOf(' '));
            if (secTarih == "Giriş")
            {
                GirisTarih.Text = showDate;
                GirisTime = sfcalender.SelectedDate.Value;
            }
            else
            {
                CikisTarih.Text = showDate;
                CikisTime = sfcalender.SelectedDate.Value;
            }

            if (GirisTime.Year != 0001 && CikisTime.Year != 0001)
            {
                var TotalTime = CikisTime.Subtract(GirisTime);
                days = TotalTime.Days;

            }
            TotalCalculate_AfterChanged(sender, e);
            CalenderGrid.IsVisible = false;
            
        }

      

        private async void Load()
        {

            var temsilciResult = await firebase.Child("Oteller").OnceAsync<HotelModel>();
            foreach (var item in temsilciResult)
                ListOteller.Add(item.Object);

            ListOteller.Add(new HotelModel { HotelAd = "Otel istemiyorum", Fiyat = 0 });

            var islemResult = await firebase.Child("MedicalIslemler").OnceAsync<MedicalIslem>();
            foreach (var item in islemResult)
                ListIslem.Add(item.Object);

            Oteller = new ObservableCollection<HotelModel>(ListOteller);
            Islemler = new ObservableCollection<MedicalIslem>(ListIslem);

            IslemP.SelectedIndexChanged += TotalCalculate_AfterChanged;
            HotelP.SelectedIndexChanged += HotelP_SelectedIndexChanged;
            Transfer.SelectedIndexChanged += TotalCalculate_AfterChanged;
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
            var hotelSender = ((Picker)sender).SelectedItem as HotelModel;
            TotalCalculate_AfterChanged(sender, e);
        }

        private void IslemP_SelectedIndexChanged(object sender, EventArgs e)
        {

            var islemSender =((Picker)sender).SelectedItem as MedicalIslem;
            var otel = HotelP.SelectedItem != null ? HotelP.SelectedItem as HotelModel : new HotelModel { Fiyat = 0 };
            var transfer = (string)Transfer.SelectedItem == "Var" ? 400 : 0;
            Total = (islemSender.Fiyat + otel.Fiyat + transfer).ToString() + " ₺";
        }

        private void TotalCalculate_AfterChanged(object sender, EventArgs e)
        {
            var secilenHotel = HotelP.SelectedItem as HotelModel;
            var secilenIslem = IslemP.SelectedItem as MedicalIslem;
            var secilenTransfer = Transfer.SelectedItem as string;
            var verilenFiyat = VerilenFiyat.Text != "" ? VerilenFiyat.Text : "0";

            var hotelFiyat = secilenHotel != null ? secilenHotel.Fiyat : 0;
            var islemFiyat = secilenIslem != null ? secilenIslem.Fiyat : 0;
            var transferFiyat= secilenTransfer == "Var" ? 400 : 0;
            var topKarFiyat = Convert.ToInt32(verilenFiyat);

            Total = ((hotelFiyat * days) + islemFiyat + transferFiyat + topKarFiyat ).ToString(); 

            DovizHesapla(Total);
        }

        XmlDocument xDoc = new XmlDocument(); //global tanınmlayın

        private void DovizHesapla(string totalTL)
        {
            string url = "http://www.tcmb.gov.tr/kurlar/today.xml";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string xmlData = wc.DownloadString(url);
            xDoc.LoadXml(xmlData);
            XmlNodeList kur = xDoc.DocumentElement.ChildNodes;
            string euroKur="1";

            List<string> kurlar = new List<string>();

            foreach (XmlNode veri in kur)
            {
                kurlar.Add(veri.ChildNodes[3].InnerText);
            }

            var decimalT = (decimal)(Convert.ToInt32(totalTL) / decimal.Parse(kurlar[3]) * 10000);
            TotalEuro = Decimal.Round(decimalT, 2).ToString();
        }

        private async void DevamButon_Clicked(object sender, EventArgs e)
        {
            var islem = IslemP.SelectedItem.ToString();
            var hotel = HotelP.SelectedItem.ToString();
            var transfer = Transfer.SelectedItem.ToString();
            var teklid = VerilenFiyat.Text;

            var HastaKayit = new KayitliHasta
            {
                Hotel = hotel,
                HastaKod = HastaTelKod,
                TemsilciKod = App.Uyg.LoginTemsilci.TemsilciKod,
                Transfer = transfer,
                VerilenTeklifEuro = VerilenFiyat.Text,
                OnayDurumu = 0,
                SonDurum = "Beklemede",
                ToplamFiyatTl = Total,
                ToplamFiyatEuro = TotalEuro
            };

            try
            {
                await firebase.Child("KayitliHasta").PostAsync(HastaKayit);
                await DisplayAlert("Eklendi", "Hasta başarılı bir şekilde eklendi", "Tamam");
                App.Current.MainPage = new TemsilciPage();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
            }


        }

        private void VerilenFiyat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (VerilenFiyat.Text == "" || VerilenFiyat.Text == null)
                return;

            TotalCalculate_AfterChanged(sender, e);
        }

        private void VazgecButon_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new TemsilciPage();
        }

        private void CalenderBox_Tapped(object sender, EventArgs e)
        {
            CalenderGrid.IsVisible = false;
        }
    }
}