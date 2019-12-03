using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuropeAesth.Model;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.ViewPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CarouselRotator : ContentView
	{
        public ObservableCollection<YaziModel> Obs_Yazi
        {
            get { return (ObservableCollection<YaziModel>)GetValue(Obs_YaziProperty); }
            set { SetValue(Obs_YaziProperty, value); }
        }
        public static readonly BindableProperty Obs_YaziProperty = BindableProperty.Create("Obs_Yazi", typeof(ObservableCollection<YaziModel>),
            typeof(CaroselViewPage), default(ObservableCollection<YaziModel>));

        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public CarouselRotator ()
		{
			InitializeComponent ();
		}
	}
}
