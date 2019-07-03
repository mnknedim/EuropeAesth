using EuropeAesth.Component;
using EuropeAesth.Pages.Yonetici;
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

          
            //UstStack.Children.Add(new ButtonView { ImageUrl = "ic_hastaKabul.png", UnderText = "Hasta Kabul", PageName = new TestPage(), TappedCommand = CallPages });
           
        }

        public Command CallPages = new Command((e) => {

            var Navigation = App.Current.MainPage.Navigation;
            var item = (ButtonView)e;
            Navigation.PushModalAsync((item.PageName));
        });

        private async void BtnGeri_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void Temsilciler_Clicked(object sender, EventArgs e)
        {

        }

        private void Hastalar_Clicked(object sender, EventArgs e)
        {

        }

        private void Yazılar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Interface.Yazilar());
        }

        private void Multimedia_Clicked(object sender, EventArgs e)
        {

        }
    }
}