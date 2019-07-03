using EuropeAesth.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace EuropeAesth
{
	public class TabbedMainPage : TabbedPage
	{
        public TabbedMainPage ()
		{
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            NavigationPage.SetHasNavigationBar(this, false);

            Children.Add(new Anasayfa() { Title = "Anasayfa", Icon = "ic_anasayfa.png" });
            Children.Add(new BanaOzel() { Title = "Bana Özel", Icon = "ic_user.png" });
            Children.Add(new Iletisim() { Title = "İletişim", Icon = "ic_iletisim.png" });
        }
	}
}