using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Storage;
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
		}

        private async void YaziList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var yaziDuzenle = e.SelectedItem as YaziModel;
            await Navigation.PushModalAsync(new YaziEkle() { Obs_Yazi = yaziDuzenle });
        }

        private async void YazilarYukle()
        {
            var tumYazilar =await firebase.Child("Yazilar").OnceAsync<YaziModel>();
            Obs_Yazi = new ObservableCollection<YaziModel>();
            if (tumYazilar != null)
            {
                foreach (var item in tumYazilar)
                {
                    Obs_Yazi.Add(item.Object);
                }
                YaziList.ItemsSource = Obs_Yazi;
                YaziList.BindingContext = Obs_Yazi;
            }
           

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new YaziEkle());
        }
    }
}