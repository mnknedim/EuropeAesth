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
            NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "Anasayfa";
           this.
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);

            NavigationPage.SetHasNavigationBar(this, false);

            Children.Add(new NavigationPage(new Anasayfa() { Title = "Anasayfa", Icon = "ic_anasayfa.png" }) { Title = "Anasayfa", Icon = "ic_anasayfa.png" });
            Children.Add(new NavigationPage(new BanaOzel() { Title = "Bana Özel", Icon = "ic_user.png" }) { Title = "Bana Özel", Icon = "ic_user.png" });
            Children.Add(new NavigationPage(new Iletisim() { Title = "İletişim", Icon = "ic_iletisim.png" }) { Title = "Bana Özel", Icon = "ic_iletisim.png" });
        }
	}
}