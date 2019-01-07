using EuropeAesth.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth
{
	public class TabbedMainPage : TabbedPage
	{
		public TabbedMainPage ()
		{
            Children.Add(new BanaOzel() { Title = "Bana Özel" });
            Children.Add(new Anasayfa() { Title = "Anasayfa" });
            Children.Add(new Iletisim() { Title = "İletişim" });
        }
	}
}