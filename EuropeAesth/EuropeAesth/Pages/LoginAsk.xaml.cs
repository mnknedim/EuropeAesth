using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuropeAesth.Renderer;
using EuropeAesth.VM;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginAsk : PopupPage
    {
		public LoginAsk (string newPage = null)// (newpage) nerden çağrıldı LoginAsk
		{
			InitializeComponent ();

            GoogleButton.BindingContext = new GoogleSignInVM() {ToPage = newPage};
        }
        private async void Giris_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BanaOzel());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}