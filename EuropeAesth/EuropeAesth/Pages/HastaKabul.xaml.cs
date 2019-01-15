using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HastaKabul : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        List<KullaniciModel> OnayBekleyenlerList = new List<KullaniciModel>();
        public HastaKabul ()
		{
            BindingContext = this;
            InitializeComponent();
            Load();
        }

        private async void Load()
        {
            var kullanicilar = await firebase.Child("Kullanicilar").OnceAsync<KullaniciModel>();
            var onayBekleyenler = kullanicilar.Where(x => x.Object.HastaKabul == false);

            foreach (var item in onayBekleyenler)
                OnayBekleyenlerList.Add(item.Object);

            LstOnayBekleyenler.BindingContext = OnayBekleyenlerList;
        }

        private void LstYeniHastalar_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as KullaniciModel;
            Navigation.PushModalAsync(new HastaBilgiGir(item));
        }
    }
}