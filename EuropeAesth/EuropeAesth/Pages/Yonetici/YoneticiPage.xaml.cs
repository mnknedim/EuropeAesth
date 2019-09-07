using EuropeAesth.Component;
using EuropeAesth.MasDetPage;
using EuropeAesth.Pages.Yonetici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        }

        public Command CallPages = new Command((e) => {

            var Navigation = App.Current.MainPage.Navigation;
            var item = (ButtonView)e;
            Navigation.PushAsync((item.PageName));
        });

        private async void Temsilciler_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new TemsilcilerPage());
        }

        private async void Hastalar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Hastalarim());
        }

        private async void Yazılar_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Interface.Yazilar());
        }

        private async void Multimedia_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Interface.Videolar());
            //await Navigation.PushAsync(new Tester());

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("UserKod");
            SecureStorage.Remove("Parola");
           App.Current.MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.FromHex("#304f72") };
        }
    }
}