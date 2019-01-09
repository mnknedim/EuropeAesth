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

           
            var hastaneButton = new ButtonView { ImageUrl = "ic_hastaKabul.png", UnderText = "Hasta Kabul" };
            var tabGest = new TapGestureRecognizer();
            
            hastaneButton.GestureRecognizers.Add(tabGest);

            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_kabulHastalar.png", UnderText = "Hastalar" });
            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_TaburcuEt.png", UnderText = "Taburcu Et" });
            AltStack.Children.Add(hastaneButton);
            AltStack.Children.Add(new ButtonView { ImageUrl = "ic_TemsilciEkle.png", UnderText = "TemsilciEkle" });
        }
	}
}