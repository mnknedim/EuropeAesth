using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.GoogleUser
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GUserPage : ContentPage
	{
        public GoogleProfile GProfil
        {
            get => (GoogleProfile)GetValue(GProfilProperty);
            set => SetValue(GProfilProperty, value);
        }
        public static readonly BindableProperty GProfilProperty =
        BindableProperty.Create(nameof(GProfil), typeof(GoogleProfile), typeof(GUserPage), default(GoogleProfile));

        public GUserPage ()
		{
			InitializeComponent ();
            BindingContext = this;
           
            GProfil = App.Uyg.GoogleGirisYapan;

		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("GoogleLogin");
            CrossGoogleClient.Current.Logout();
            App.Current.MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.FromHex("#304f72") };

        }
    }
}