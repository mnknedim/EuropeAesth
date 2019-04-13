using EuropeAesth.Component;
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
	public partial class YoneticiPage : ContentPage
	{
        

        public object TapSenderObejct { get; set; }
        public YoneticiPage ()
		{
			InitializeComponent ();

            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_kabulHastalar.png", UnderText = "Hastalar",PageName = new KabulHastalar() , TappedCommand = CallPages });
            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_TaburcuEt.png", UnderText = "Taburcu Et", PageName = new TaburcuEt() , TappedCommand = CallPages });
            AltStack.Children.Add(new ButtonView { ImageUrl = "ic_hastaKabul.png", UnderText = "Hasta Kabul", PageName = new TestPage(), TappedCommand = CallPages });
            AltStack.Children.Add(new ButtonView { ImageUrl = "ic_TemsilciEkle.png", UnderText = "TemsilciEkle", PageName = new TemsilciEkle(), TappedCommand = CallPages });

            Alt2Stack.Children.Add(new ButtonView { ImageUrl = "ic_Temsilciler.png", UnderText = "Temsilciler", PageName = new TemsilcilerPage(), TappedCommand = CallPages });
        }

        public Command CallPages = new Command((e) => {

            var Navigation = App.Current.MainPage.Navigation;
            var item = (ButtonView)e;
            Navigation.PushModalAsync((item.PageName));
        });
	}
}