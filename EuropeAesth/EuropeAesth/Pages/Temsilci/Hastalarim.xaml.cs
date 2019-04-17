using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Temsilci
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Hastalarim : ContentPage
	{
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        List<TemsilciModel> temsilciler = new List<TemsilciModel>();
        public Hastalarim ()
		{
			InitializeComponent ();
		}
	}
}