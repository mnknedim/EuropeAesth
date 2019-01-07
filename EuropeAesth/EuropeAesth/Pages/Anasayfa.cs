using EuropeAesth.ViewPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.Pages
{
	public class Anasayfa : ContentPage
	{
		public Anasayfa ()
		{
            Content = new StackLayout {
                Children = {
                    new CaroselViewPage(),
                    new ScrollViewPage(),
				}
			};
		}
	}
}