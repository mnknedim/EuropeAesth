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

            
        }

        private void TabGest_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ButtonAciklama());
        }
    }
}