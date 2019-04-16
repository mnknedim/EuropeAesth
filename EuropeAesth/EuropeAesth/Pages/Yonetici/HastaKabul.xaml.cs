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
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        List<KullaniciHasta> bekleyenHastalar = new List<KullaniciHasta>();
        public HastaKabul ()
		{
            BindingContext = this;
            InitializeComponent();
            Load();
        }

        private async void Load()
        {
            var bekleyenResult = await firebase.Child("KayitliHastalar").OnceAsync<KullaniciHasta>();
            foreach (var item in bekleyenResult)
                bekleyenHastalar.Add(item.Object);

            LstTBekleyenHastalar.BindingContext = bekleyenHastalar;
        }
    }
}