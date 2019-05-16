using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
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
	public partial class TemsilcilerPage : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        List<AllUser> temsilciler = new List<AllUser>();
        public TemsilcilerPage()
		{
            BindingContext = this;
			InitializeComponent ();
            Load();
		}

        private async void Load()
        {
            var temsilciResult = await firebase.Child("AllUser").OnceAsync<AllUser>();
            foreach (var item in temsilciResult)
                if (item.Object.YetkiKod == 2)
                {
                    temsilciler.Add(item.Object);
                }

            LstTemsilci.BindingContext = temsilciler;
        }

        private async void LstTemsilci_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tmslc = (AllUser)e.Item;

            await Navigation.PushModalAsync(new TemsilciDetail(tmslc));
        }
    }
}