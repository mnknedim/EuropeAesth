using EuropeAesth.Helpers;
using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
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

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create("IsLoading", typeof(bool),
            typeof(MenuYazilar), false);


        public ObservableCollection<YaziModel> AllText;
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

                YaziList.IsVisible = true;
                AllText = new ObservableCollection<YaziModel>(Obs_Yazi);
                //ActivityIndicator.IsVisible = false;
            }
        }

        private async void YaziList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedYazi = e.Item as YaziModel;
            await Navigation.PushAsync(new ListViewDetail() { SecYazi = selectedYazi });
            YaziList.SelectedItem = null;
        }

        Stopwatch st = new Stopwatch();
        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            var item = (Entry)sender;
            if (item.Text.Count() >= 3)
            {
                IsLoading = true;
                await Task.Delay(500);

                var filtered = AllText.Where(x => (x.Baslik.ToLowerWithUtf()).IndexOf(e.NewTextValue.ToLowerWithUtf() ,StringComparison.OrdinalIgnoreCase) >= 0 ||  (x.Aciklama.ToLowerWithUtf()).IndexOf(e.NewTextValue.ToLowerWithUtf(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                //foreach (var yazi in filtered)
                //{
                //    var baslikUtf = yazi.Baslik.ToLowerWithUtf();
                //    var selectedIndex = baslikUtf.IndexOf(e.NewTextValue?.ToLowerWithUtf());
                //    Span first = new Span { Text = yazi.Baslik.Substring(0, selectedIndex) }; 
                //    Span selected = new Span { Text = yazi.Baslik.Substring(selectedIndex, e.NewTextValue.Count()) };
                //    Span last = new Span { Text = yazi.Baslik.Substring(selectedIndex + e.NewTextValue.Count(), baslikUtf.Count() - (selectedIndex + e.NewTextValue.Count())) };
                //    selected.TextColor = Color.Red;
                //    var formattedStr = new FormattedString();
                //    formattedStr.Spans.Add(first);
                //    formattedStr.Spans.Add(selected);
                //    formattedStr.Spans.Add(last);

                //}
                Obs_Yazi = new ObservableCollection<YaziModel>(filtered);
            }
            if (item.Text.Count() <= 3 && e.OldTextValue?.Count() == 3)
            {
                Obs_Yazi = new ObservableCollection<YaziModel>(AllText);
            }

            IsLoading = false;
        }
    }
}