using EuropeAesth.Component;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.ViewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScrollViewPage : ContentView
    {
        public ObservableCollection<YaziModel> Obs_Yazi
        {
            get { return (ObservableCollection<YaziModel>)GetValue(Obs_YaziProperty); }
            set { SetValue(Obs_YaziProperty, value); }
        }
        public static readonly BindableProperty Obs_YaziProperty = BindableProperty.Create("Obs_Yazi", typeof(ObservableCollection<YaziModel>),
            typeof(ScrollViewPage), default(ObservableCollection<YaziModel>));

        ObservableCollection<ImageScrollViewModel> ScrollImages = new ObservableCollection<ImageScrollViewModel>();
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public ScrollViewPage()
        {
            InitializeComponent();
            YazilarYukle();
        }

        private async void YazilarYukle()
        {
            var tumYazilar = await firebase.Child("Yazilar").OnceAsync<YaziModel>();
            var orderedYazilar = tumYazilar.OrderByDescending(x => x.Object.Tarih).ToList();
            Obs_Yazi = new ObservableCollection<YaziModel>();
            if (tumYazilar != null)
            {
                foreach (var item in orderedYazilar)
                {
                    Obs_Yazi.Add(item.Object);
                }
                YaziList.ItemsSource = Obs_Yazi;
                YaziList.BindingContext = Obs_Yazi;
            }
        }

        private void Lst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedItem = (ImageScrollViewModel)e.SelectedItem;
            App.Current.MainPage = new ListViewDetail(SelectedItem);
        }
    }
}