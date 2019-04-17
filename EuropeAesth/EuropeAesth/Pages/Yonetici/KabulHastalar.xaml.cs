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
    public partial class KabulHastalar : ContentPage
    {
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        List<KabulHastalar> kabulHastalar = new List<KabulHastalar>();
        public KabulHastalar()
        {
            BindingContext = this;
            InitializeComponent();
            Load(); 
        }

        private async void Load()
        {
            var kayitliHastaResult = await firebase.Child("KayitliHastalar").OnceAsync<KabulHastalar>();
            foreach (var item in kayitliHastaResult)
                kabulHastalar.Add(item.Object);

            LstKabulHastalar.BindingContext = kabulHastalar;
        }
    }
}