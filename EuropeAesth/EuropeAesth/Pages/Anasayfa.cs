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
            var st = new StackLayout
            {
                Children =
                {
                     new CaroselViewPage(),
                    new ScrollViewPage(),
                }
            };
            if (Device.RuntimePlatform == Device.iOS)
            {
                st.Margin = new Thickness(0, 20);
            }

            Content = st;
		}
	}
}