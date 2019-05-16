using Acr.UserDialogs;
using EuropeAesth.Model;
using EuropeAesth.Pages.Temsilci;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HastaEklePage : ContentPage
    {
        public ObservableCollection<Country> Ulkeler
        {
            get { return (ObservableCollection<Country>)GetValue(UlkelerProperty); }
            set { SetValue(UlkelerProperty, value); }
        }
        public static readonly BindableProperty UlkelerProperty = BindableProperty.Create("Ulkeler", typeof(ObservableCollection<Country>),
            typeof(IslemPage), default(ObservableCollection<Country>));

        public ObservableCollection<States> Sehirler
        {
            get { return (ObservableCollection<States>)GetValue(SehirlerProperty); }
            set { SetValue(SehirlerProperty, value); }
        }
        public static readonly BindableProperty SehirlerProperty = BindableProperty.Create("Sehirler", typeof(ObservableCollection<States>),
            typeof(IslemPage), default(ObservableCollection<States>));
        public HastaEklePage()
        {
            InitializeComponent();
            BindingContext = this;
            UlkeP.SelectedIndexChanged += UlkeP_SelectedIndexChanged;
            UlkeSehirLoad();
            HTelefon.TextChanged += HTelefon_TextChanged;
            
        }

        private void UlkeP_SelectedIndexChanged(object sender, EventArgs e)
        {
            stream = assembly.GetManifestResourceStream("EuropeAesth.Helpers.sehirler.json");

            using (var reader = new StreamReader(stream))
            {

                var json = reader.ReadToEnd();
                var SelectedCountry = UlkeP.SelectedItem as Country;
                var data = JsonConvert.DeserializeObject<StatesResponse>(json).states.Where(x => x.country_id == SelectedCountry.id).ToList();

                Sehirler = new ObservableCollection<States>(data);
            }
        }

        Stream stream;
        Assembly assembly;
        private void UlkeSehirLoad()
        {
            assembly = typeof(HastaEklePage).GetTypeInfo().Assembly;
            stream = assembly.GetManifestResourceStream("EuropeAesth.Helpers.ulkeler.json");

            using (var reader = new StreamReader(stream))
            {

                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<CountryResponse>(json).countries.ToList();

                Ulkeler = new ObservableCollection<Country>(data);
            }

            stream.Close();

            

        }

        private void HTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //if ((e.OldTextValue == "" || e.OldTextValue == null) && e.NewTextValue == "0")
            //    HTelefon.Text = "";
            //if (HTelefon.Text.Length > 10)
            //    HTelefon.Text = e.OldTextValue;
        }

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Lütfen Bekleyiniz...", MaskType.None);
            FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

            var kullaniciHastalar = await firebase.Child("KullaniciHastalar").OnceAsync<KullaniciHasta>();
            var KayitliHastalar = await firebase.Child("KayitliHasta").OnceAsync<KayitliHasta>();

            var kullaniciH = kullaniciHastalar.FirstOrDefault(x => x.Object.Telefon == HTelefon.Text);

            if (kullaniciH != null)
            {
                var kayitliMi = KayitliHastalar.Any(x => x.Object.HastaId == kullaniciH.Object.Id);

                if (kayitliMi)
                {
                    await DisplayAlert("Lütfen Tel değiştirin", "Bu telefon numarasına ait kayitli hasta var.", "Tamam");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                else
                {
                    var devamEt = await DisplayAlert("Bu Tlf No Kayıtlı", "Bu telefona ait başka bir hasta kayıtlı bulunuyor! Teklife devam etmek istemisiniz?", "Devam", "Vageç");
                    if (devamEt)
                    {
                        var hastaId = kullaniciH.Object.Id;
                        await Navigation.PushModalAsync(new IslemPage(hastaId));

                    }
                    else
                        return;
                }
            }
            else
            {
                var HastaEkle = new KullaniciHasta()
                {
                    Id = Guid.NewGuid(),
                    AdSoyad = HAdSoyad.Text,
                    Email = HEmail.Text,
                    Telefon = HTelefon.Text,
                    Ulke = (UlkeP.SelectedItem as Country).name,
                    YetkiKod = 3,
                    Şehir = (SehirP.SelectedItem as States).name,
                    TemsilciKod = App.Uyg.LoginUser.UserKod,

                };

                try
                {
                    await firebase.Child("KullaniciHastalar").PostAsync(HastaEkle);
                    await DisplayAlert("Başarılı", "Hasta başarılı bir şekilde Eklendi", "Tamam");
                    await Navigation.PushModalAsync(new IslemPage(HastaEkle.Id));

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
                }
            }






           

            //bool kullaniciMi = false;
            //bool kayitliMi = false;

            //if (kullaniciHastalar != null && KayitliHastalar != null)
            //{
               

            //    if (kullaniciMi != null)
            //    {

            //    }

            //    kayitliMi = KayitliHastalar.FirstOrDefault(x => x.Object.Id == HTelefon.Text).Object.Telefon; 


            //}


        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new TemsilciPage();
        }

        private void HTelefon_Focused(object sender, FocusEventArgs e)
        {
            if (HTelefon.Text == "" || HTelefon.Text == null)
            {
                HTelefon.Text = "+";

            }
        }
    }
}