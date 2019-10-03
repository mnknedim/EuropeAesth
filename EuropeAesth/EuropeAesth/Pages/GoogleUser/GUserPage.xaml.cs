using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuropeAesth.VM;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.GoogleUser
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GUserPage : ContentPage
	{
        public Model.GoogleUser GProfil
        {
            get => (Model.GoogleUser)GetValue(GProfilProperty);
            set => SetValue(GProfilProperty, value);
        }
        public static readonly BindableProperty GProfilProperty =
        BindableProperty.Create(nameof(GProfil), typeof(Model.GoogleUser), typeof(GUserPage), default(Model.GoogleUser));

        public GUserPage (Model.GoogleUser googleProfile)
		{
			InitializeComponent ();
            BindingContext = this;

            GProfil = googleProfile;

            CikisButon.BindingContext = new GoogleSignInVM();

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("GoogleLogin");
            App.Current.MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.FromHex("#304f72") };

        }
    }
}