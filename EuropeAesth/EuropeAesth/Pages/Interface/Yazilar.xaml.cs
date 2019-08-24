using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Interface
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Yazilar : ContentPage
	{
        public ObservableCollection<YaziModel> Obs_Yazi
        {
            get { return (ObservableCollection<YaziModel>)GetValue(Obs_YaziProperty); }
            set { SetValue(Obs_YaziProperty, value); }
        }
        public static readonly BindableProperty Obs_YaziProperty = BindableProperty.Create("Obs_Yazi", typeof(ObservableCollection<YaziModel>),
            typeof(Yazilar), default(ObservableCollection<YaziModel>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public Yazilar ()
		{
			InitializeComponent ();
            
            BindingContext = this;
            YazilarYukle();
            YaziList.ItemSelected += YaziList_ItemSelected;

            //MessagingCenter.Subscribe<string>(this, "UpdateOrInsertOrDelete", (sender) => {
            //    YazilarYukle();
            //});
        }

        private async void YaziList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var yaziDuzenle = e.SelectedItem as YaziModel;
            await Navigation.PushModalAsync(new YaziEkle() { Obs_Yazi = yaziDuzenle, Duzenle = true });

        }

        private async void YazilarYukle()
        {
            var tumYazilar =await firebase.Child("Yazilar").OnceAsync<YaziModel>();
            //tumYazilar = tumYazilar.OrderByDescending(x => x.Object.Tarih).ToList();
            Obs_Yazi = new ObservableCollection<YaziModel>();
            if (tumYazilar != null)
            {
                foreach (var item in tumYazilar)
                {
                    item.Object.Id = item.Key;
                    Obs_Yazi.Add(item.Object);
                    
                }
                Obs_Yazi = new ObservableCollection<YaziModel>(Obs_Yazi.OrderByDescending(x => x.Tarih).ToList());
                //YaziList.ItemsSource = Obs_Yazi;
                //YaziList.BindingContext = Obs_Yazi;
            }

           CheckChange();
        }

        private void CheckChange()
        {
            firebase.Child("Yazilar")
                .AsObservable<YaziModel>()
                .Where(yazi => yazi.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                .Subscribe(yazi =>
                {
                    var rs = Obs_Yazi.Any(x=>x.Id == yazi.Key);
                    if (!rs)//Insert
                    {
                        yazi.Object.Id = yazi.Key;
                        Obs_Yazi.Add(yazi.Object);
                        Obs_Yazi = new ObservableCollection<YaziModel>(Obs_Yazi.OrderByDescending(x => x.Tarih).ToList());
                       
                    }
                    else//update
                    {
                        var item = Obs_Yazi.Where(x => x.Id == yazi.Key).FirstOrDefault();
                        var itemIndex = Obs_Yazi.IndexOf(item);
                        if (yazi.Object.Tarih != item.Tarih)
                        {
                            Obs_Yazi[itemIndex] = yazi.Object;
                            Obs_Yazi = new ObservableCollection<YaziModel>(Obs_Yazi.OrderByDescending(x => x.Tarih).ToList());
                        }

                    }
                });

            firebase.Child("Yazilar")
                .AsObservable<YaziModel>()
                .Where(yazi =>  yazi.EventType == Firebase.Database.Streaming.FirebaseEventType.Delete)
                .Subscribe(async yazi =>
                {
                    List<YaziModel> yazis = new List<YaziModel>();
                    var tumYazilar = await firebase.Child("Yazilar").OnceAsync<YaziModel>();
                    foreach (var item in tumYazilar)
                        yazis.Add(item.Object);

                    Obs_Yazi = new ObservableCollection<YaziModel>(yazis.OrderByDescending(x => x.Tarih).ToList());
                    
                });

        }


        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new YaziEkle());
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == Obs_YaziProperty.PropertyName)
            {

            }
        }
    }
}