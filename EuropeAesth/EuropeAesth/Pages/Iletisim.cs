using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.Pages
{
	public class Iletisim : ContentPage
	{
		public Iletisim ()
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "İletişim" }
				}
			};
		}
	}
}