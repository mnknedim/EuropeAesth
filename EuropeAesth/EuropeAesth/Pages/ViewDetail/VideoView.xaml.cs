using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Firebase.Database;
using Firebase.Database.Query;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.ViewDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoView : ContentPage
	{
        public VideoModel SecVideo
        {
            get { return (VideoModel)GetValue(SecVideoProperty); }
            set { SetValue(SecVideoProperty, value); }
        }
        public static readonly BindableProperty SecVideoProperty =
            BindableProperty.Create(nameof(SecVideo), typeof(VideoModel), typeof(VideoView), default(VideoModel));

        public bool FullScreen
        {
            get => (bool)GetValue(FullScreenProperty);
            set => SetValue(FullScreenProperty, value);
        }
        public static readonly BindableProperty FullScreenProperty = BindableProperty.Create("FullScreen", typeof(bool), typeof(VideoView), true);

        public ObservableCollection<YorumlarModel> Yorumlar
        {
            get { return (ObservableCollection<YorumlarModel>)GetValue(YorumlarProperty); }
            set { SetValue(YorumlarProperty, value); }
        }
        public static readonly BindableProperty YorumlarProperty =
            BindableProperty.Create(nameof(Yorumlar), typeof(ObservableCollection<YorumlarModel>), typeof(VideoView), default(ObservableCollection<YorumlarModel>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public VideoView ()
		{
			InitializeComponent ();
            BindingContext = this;
            LoadYorumlar();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                videoPlayer.VerticalOptions = LayoutOptions.FillAndExpand;
                videoPlayer.HorizontalOptions = LayoutOptions.FillAndExpand;
                DetayStack.IsVisible = false;
                FullScreen = false;

            }
            else
            {
                FullScreen = true;
                DetayStack.IsVisible = true;
                videoPlayer.VerticalOptions = LayoutOptions.Start;
            }
        }
        private async void LoadYorumlar()
        {
            var response = await firebase.Child("Yorumlar").OnceAsync<YorumlarModel>();
            var result = response.Where(x => x.Object.YaziId == SecVideo.Id && x.Object.Onayli == true);
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
                    YaziId = SecVideo.Id,
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
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == SecVideoProperty.PropertyName)
            {
                videoPlayer.Source = SecVideo.VideoUrl;
            }
        }

    }
}