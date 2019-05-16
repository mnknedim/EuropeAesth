using Acr.UserDialogs;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
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
	public partial class TemsilciEkle : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public ObservableCollection<Country> Ulkeler
        {
            get { return (ObservableCollection<Country>)GetValue(UlkelerProperty); }
            set { SetValue(UlkelerProperty, value); }
        }
        public static readonly BindableProperty UlkelerProperty = BindableProperty.Create("Ulkeler", typeof(ObservableCollection<Country>),
            typeof(TemsilciEkle), default(ObservableCollection<Country>));

        public ObservableCollection<States> Sehirler
        {
            get { return (ObservableCollection<States>)GetValue(SehirlerProperty); }
            set { SetValue(SehirlerProperty, value); }
        }
        public static readonly BindableProperty SehirlerProperty = BindableProperty.Create("Sehirler", typeof(ObservableCollection<States>),
            typeof(TemsilciEkle), default(ObservableCollection<States>));
        public TemsilciEkle ()
		{
            
			InitializeComponent ();
            BindingContext = this;
            UlkeP.SelectedIndexChanged += UlkeP_SelectedIndexChanged; ;
            UlkeSehirLoad();
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

        private async void Kayit_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Kayıt ediliyor", MaskType.Gradient);
            var kayitKontrol = await firebase.Child("AllUser").OnceAsync<AllUser>();
            if (kayitKontrol == null)
                return;
            else
            {
                if (kayitKontrol.Any(x=>x.Object.UserKod == UserKod.Text))
                {
                    await DisplayAlert("Kayıt Başarısız","Bu Username ile kayıt bulunuyor. Farklı deneyiniz","");
                    return;
                }
            }

            var Temsilci = new AllUser()
            {
                UserKod = UserKod.Text,
                YetkiKod = 2,
                Email = Email.Text,
                AdSoyad = AdSoyad.Text,
                Parola = Parola.Text,
                Telefon = Telefon.Text,
                Sehir = (SehirP.SelectedItem as States).name,
                Ulke = (UlkeP.SelectedItem as Country).name,
            };

            try
            {
                await firebase.Child("AllUser").PostAsync(Temsilci);
                await DisplayAlert("Kayıt", "Başarılı", "Tamam");
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. ({ex.Data.ToString()})", "Tamam");
            }
            UserDialogs.Instance.HideLoading();

        }

        private void Vazgec_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}