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

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HastaBilgiGir : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvan-9b15c.firebaseio.com/");
        ObservableCollection<HastahaneModel> HastahaneList = new ObservableCollection<HastahaneModel>();
        ObservableCollection<HotelModel> OtelList = new ObservableCollection<HotelModel>();
		public HastaBilgiGir (HastaModel)
		{
            BindingContext = this;
			InitializeComponent ();
            HastahanePicker.BindingContext = HastahaneList;
            OtelPicker.BindingContext = OtelList;
            Load();

        }
        public async void Load()
        {
            var hastahaneler =await firebase.Child("Hastahaneler").OnceAsync<HastahaneModel>();
            foreach (var item in hastahaneler)
                HastahaneList.Add(item.Object);

            var oteller = await firebase.Child("Oteller").OnceAsync<HotelModel>();
            foreach (var item in oteller)
                OtelList.Add(item.Object);
        }
    }
}