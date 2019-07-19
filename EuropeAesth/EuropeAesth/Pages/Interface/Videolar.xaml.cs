using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Interface
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Videolar : ContentPage
	{
        public ObservableCollection<VideoModel> Obs_Video
        {
            get { return (ObservableCollection<VideoModel>)GetValue(Obs_VideoProperty); }
            set { SetValue(Obs_VideoProperty, value); }
        }
        public static readonly BindableProperty Obs_VideoProperty = BindableProperty.Create("Obs_Video", typeof(ObservableCollection<VideoModel>),
            typeof(Videolar), default(ObservableCollection<VideoModel>));

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VideoEkle());
        }
        public bool FullScreen
        {
            get => (bool)GetValue(FullScreenProperty);
            set => SetValue(FullScreenProperty, value);
        }
        public static readonly BindableProperty FullScreenProperty = BindableProperty.Create("FullScreen", typeof(bool), typeof(Videolar), true);

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public Videolar()
        {
            InitializeComponent();

            BindingContext = this;
            VideolarYukle();
            VideoList.ItemSelected += VideoList_ItemSelected;
        }

        private async void VideoList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var duzenleVideo = e.SelectedItem as VideoModel;
            await Navigation.PushModalAsync(new VideoEkle() { Obs_Video = duzenleVideo });
        }

        private async void VideolarYukle()
        {
            var tumVideolar = await firebase.Child("Videolar").OnceAsync<VideoModel>();
            Obs_Video = new ObservableCollection<VideoModel>();
            if (tumVideolar != null)
            {
                foreach (var item in tumVideolar)
                {
                    Obs_Video.Add(item.Object);
                }
                VideoList.ItemsSource = Obs_Video;
                VideoList.BindingContext = Obs_Video;
            }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                videostack.VerticalOptions = LayoutOptions.FillAndExpand;
                videostack.HorizontalOptions = LayoutOptions.FillAndExpand;
                FullScreen = false;

            }
            else
            {
                FullScreen = true;
                videostack.VerticalOptions = LayoutOptions.StartAndExpand;
            }
        }

    }
}