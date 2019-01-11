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
	public partial class UserPage : ContentPage
	{
		public UserPage ()
		{
			InitializeComponent ();

            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_userLogo.png", UnderText = "Bilgilerim" });
            UstStack.Children.Add(new ButtonView { ImageUrl = "ic_transfer.png", UnderText = "Transfer" });
            AltStack.Children.Add(new ButtonView { ImageUrl = "ic_hastane.png", UnderText = "Hastahane" });
            AltStack.Children.Add(new ButtonView { ImageUrl = "ic_hotel.png", UnderText = "Hotel" });
        }

        private void TabGest_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ButtonAciklama());
        }
    }
}