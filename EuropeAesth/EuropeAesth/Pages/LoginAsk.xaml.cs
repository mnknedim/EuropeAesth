using EuropeAesth.Model;
using Plugin.GoogleClient;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
	public partial class LoginAsk : PopupPage
	{
		public LoginAsk ()
		{
			InitializeComponent ();
            var tabGest = new TapGestureRecognizer();
            tabGest.Tapped += TabGest_Tapped;
            GoogleButton.GestureRecognizers.Add(tabGest);
		}

        private async void TabGest_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await CrossGoogleClient.Current.LoginAsync();
                GoogleProfile Googleuser = new GoogleProfile
                {
                    Email = response.Data.Email,
                    FamilyName = response.Data.FamilyName,
                    GivenName = response.Data.GivenName,
                    Id = response.Data.Id,
                    Name = response.Data.Name,
                    Picture = response.Data.Picture
                };
                MessagingCenter.Send<GoogleProfile>(Googleuser, "GoogleUser");
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        private async void Giris_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BanaOzel());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}