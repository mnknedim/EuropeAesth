using Acr.UserDialogs;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PMSPirelli.Renderer;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Interface
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YaziEkle : ContentPage
	{
        public YaziModel Obs_Yazi
        {
            get { return (YaziModel)GetValue(Obs_YaziProperty); }
            set { SetValue(Obs_YaziProperty, value); }
        }
        public static readonly BindableProperty Obs_YaziProperty = BindableProperty.Create("Obs_Yazi", typeof(YaziModel),
            typeof(YaziEkle), default(YaziModel));
        public bool Duzenle
        {
            get { return (bool)GetValue(DuzenleProperty); }
            set { SetValue(DuzenleProperty, value); }
        }
        public static readonly BindableProperty DuzenleProperty = BindableProperty.Create("Duzenle", typeof(bool),
            typeof(YaziEkle), false);

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public YaziEkle ()
		{
			InitializeComponent ();
            BindingContext = this;
            var tabGest = new TapGestureRecognizer();
            tabGest.Tapped += ResimYukle_Tabbed;
            YaziResmi.GestureRecognizers.Add(tabGest);
            DefaultResim.GestureRecognizers.Add(tabGest);
            LblYayinTarih.Text = DateTime.Now.ToString("dd.MM.yyyy");
		}

        private void BasTarih_Tapped(object sender, EventArgs e)
        {
            CalenderGrid.IsVisible = true;
        }

        private void Calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            var sfcalender = sender as SfCalendar;
            var dateString = sfcalender.SelectedDate.Value.ToString();
            var showDate = dateString.Substring(0, dateString.IndexOf(' '));
            var dateArray = showDate.Split('.');

            LblYayinTarih.Text = showDate;
            CalenderGrid.IsVisible = false;

        }
        private void CalenderBox_Tapped(object sender, EventArgs e)
        {
            CalenderGrid.IsVisible = false;
        }

        MediaFile file;
        static IImageResizer _resizer = DependencyService.Get<IImageResizer>();
        private async void ResimYukle_Tabbed(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                YaziResmi.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                YaziResmi.IsVisible = true;
            }
            catch (Exception ex)
            {
               
            }
        }

        private async Task<string> StoreImages(Stream stream)
        {
            byte[] OrgByte;
            Stream st = file.GetStream();
            using (BinaryReader br = new BinaryReader(st))
            {
                OrgByte = br.ReadBytes((int)st.Length);
            }
            var resizedByte750 = _resizer.ResizeImage(OrgByte, 750, 750).Item1;

            string stroageImage;
            using (Stream stt= new MemoryStream(resizedByte750))
            {
                var ImageUrl = Guid.NewGuid().ToString();
                stroageImage = await new FirebaseStorage("adjuvanclinic.appspot.com")
                    .Child("AdjuvanImages")
                    .Child(ImageUrl)
                      .PutAsync(stt);
            }

            string imgurl = stroageImage;
            return imgurl;
        }

        private async void Yayinla_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Lütfen Bekleyiniz...", MaskType.Gradient);
            var ImageName = Guid.NewGuid();
            ImageName.ToString();

            var result = await StoreImages(file.GetStream());

            var EklenecekYazi = new YaziModel()
            {
                Baslik = YaziBaslik.Text,
                Aciklama = YaziAciklama.Text,
                ImageUrl = result + ".png",
                Tarih = DateTime.Now
            };
            
            try
            {
                if (result != null)
                {
                    await firebase.Child("Yazilar").PostAsync(EklenecekYazi);
                    await DisplayAlert("Kaydedildi", "Kayıt Başarılı", "Tamam");
                    await Navigation.PopModalAsync();
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Hata oluştu. Tekrar Deneyiniz. {ex.Message}","Tamam");
            }
            
        }

        private async void Vazgec_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}