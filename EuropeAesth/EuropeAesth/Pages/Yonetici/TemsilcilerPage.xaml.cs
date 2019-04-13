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
        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        List<TemsilciModel> temsilciler = new List<TemsilciModel>();
        public TemsilcilerPage()
		{
            BindingContext = this;
			InitializeComponent ();
            Load();
		}

        private async void Load()
        {
            var temsilciResult = await firebase.Child("Temsilciler").OnceAsync<TemsilciModel>();
            foreach (var item in temsilciResult)
                temsilciler.Add(item.Object);

            LstTemsilci.BindingContext = temsilciler;
        }

        private async void LstTemsilci_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tmslc = (TemsilciModel)e.Item;

            await Navigation.PushModalAsync(new TemsilciView() { Temsilci = tmslc });
        }
    }
}