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

namespace EuropeAesth.Pages.MenuPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuYazilar : ContentPage
	{
        public ObservableCollection<YaziModel> Obs_Yazi
        {
            get { return (ObservableCollection<YaziModel>)GetValue(Obs_YaziProperty); }
            set { SetValue(Obs_YaziProperty, value); }
        }
        public static readonly BindableProperty Obs_YaziProperty = BindableProperty.Create("Obs_Yazi", typeof(ObservableCollection<YaziModel>),
            typeof(MenuYazilar), default(ObservableCollection<YaziModel>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public MenuYazilar ()
		{
			InitializeComponent ();
            BindingContext = this;
            YazilarYukle();
           // YaziList.ItemTapped += YaziList_ItemSelected;
        }

        private async void YazilarYukle()
        {
            var tumYazilar = await firebase.Child("Yazilar").OnceAsync<YaziModel>();
            var tumYaziSirali = tumYazilar.OrderByDescending(x => x.Object.Tarih).ToList();
            Obs_Yazi = new ObservableCollection<YaziModel>();
            if (tumYazilar != null)
            {
                foreach (var item in tumYaziSirali)
                {
                    Obs_Yazi.Add(item.Object);
                } 
            }
        }

        private async void YaziList_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            var selectedYazi = e.Item as YaziModel;
            await Navigation.PushAsync(new ListViewDetail() { SecYazi = selectedYazi });
            YaziList.SelectedItem = null;
        }
    }
}