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

            var browser = new WebView();
            browser.Source = "https://adjuvanclinic.com/i%CC%87leti%C5%9Fim";


            Content = browser;
		}
	}
}