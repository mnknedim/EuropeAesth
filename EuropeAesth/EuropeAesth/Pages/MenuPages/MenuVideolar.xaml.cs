using EuropeAesth.Model;
using EuropeAesth.Pages.ViewDetail;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuVideolar : ContentPage
    {
        public ObservableCollection<VideoModel> Obs_Video
        {
            get { return (ObservableCollection<VideoModel>)GetValue(Obs_VideoProperty); }
            set { SetValue(Obs_VideoProperty, value); }
        }
        public static readonly BindableProperty Obs_VideoProperty = BindableProperty.Create("Obs_Video", typeof(ObservableCollection<VideoModel>),
            typeof(MenuVideolar), default(ObservableCollection<VideoModel>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public MenuVideolar()
        {
            InitializeComponent();

            BindingContext = this;
            VideolarYukle();
            VideoList.ItemSelected += VideoList_ItemSelected;
        }

        private async void VideoList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedVideo = e.SelectedItem as VideoModel;
            await Navigation.PushAsync(new VideoView() { SecVideo = selectedVideo });
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
    }
}