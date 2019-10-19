using Acr.UserDialogs;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewDetail : ContentPage
	{
        public double CaroselHeight
        {
            get => (double)GetValue(CaroselHeightProperty);
            set => SetValue(CaroselHeightProperty, value);
        }
        public static readonly BindableProperty CaroselHeightProperty =
            BindableProperty.Create(nameof(CaroselHeight), typeof(double), typeof(ListViewDetail), default(double));

        public YaziModel SecYazi
        {
            get { return (YaziModel)GetValue(SecYaziProperty); }
            set { SetValue(SecYaziProperty, value); }
        }
        public static readonly BindableProperty SecYaziProperty =
            BindableProperty.Create(nameof(SecYazi), typeof(YaziModel), typeof(ListViewDetail), default(YaziModel));

        public ObservableCollection<YorumlarModel> Yorumlar
        {
            get { return (ObservableCollection<YorumlarModel>)GetValue(YorumlarProperty); }
            set { SetValue(YorumlarProperty, value); }
        }
        public static readonly BindableProperty YorumlarProperty =
            BindableProperty.Create(nameof(Yorumlar), typeof(ObservableCollection<YorumlarModel>), typeof(ListViewDetail), default(ObservableCollection<YorumlarModel>));

        DisplayInfo displayInfo;
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public ListViewDetail ()
		{
			InitializeComponent ();
            displayInfo = DeviceDisplay.MainDisplayInfo;
            CaroselHeight = displayInfo.Height / 7 ;
            BindingContext = this;
            //if (App.Uyg.GoogleGirisYapan != null || App.Uyg.LoginUser != null)
            //{
            //    EditorStack.IsEnabled = false;
            //    YorumEditor.Text = "Yorum yapmak için giriş yapın";
            //}
            LoadYorumlar();
        }

        private async void LoadYorumlar()
        {
            var response = await firebase.Child("Yorumlar").OnceAsync<YorumlarModel>();
            var result = response.Where(x => x.Object.YaziId == SecYazi.Id && x.Object.Onayli == true);
            List<YorumlarModel> yorumList = new List<YorumlarModel>();

            foreach (var item in result)
                yorumList.Add(item.Object);

            Yorumlar = new ObservableCollection<YorumlarModel>(yorumList);
            YorumlarList.HeightRequest = Yorumlar.Count * 175;

            var userName = await SecureStorage.GetAsync("UserKod");
            var gLogin = await SecureStorage.GetAsync("GoogleLogin");
            if (userName == null && gLogin == null)
            {
                EditorStack.IsVisible = false;
                GirisYapStack.IsVisible = true;
            }
            // YorumlarList.BindingContext = Yorumlar;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Bekleyin", MaskType.None);
            try
            {
                var dateTime = DateTime.Now.ToString("dd.MM.yyyy");
                var yorum = new YorumlarModel
                {
                    UserName = App.Uyg.GoogleGirisYapan.Name ?? "Admin",
                    YorumText = YorumEditor.Text,
                    YaziId = SecYazi.Id,
                    DateTime = dateTime,
                    YildizPuan = Convert.ToInt32(YildizPuan.Value),
                    Onayli = true
                };

                await firebase.Child("Yorumlar").PostAsync(yorum);
                Yorumlar.Add(yorum);
                YorumEditor.Text = "";
                YorumlarList.ItemsSource = Yorumlar;
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var page = this;
            await Navigation.PushPopupAsync(new LoginAsk(), true);

            EditorStack.IsVisible = true;
            GirisYapStack.IsVisible = false;
        }
    }
}