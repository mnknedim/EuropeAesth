using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.MenuPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuYazilarOnizleme : ContentPage
	{
        public YaziModel Yazi
        {
            get { return (YaziModel)GetValue(YaziProperty); }
            set { SetValue(YaziProperty, value); }
        }
        public static readonly BindableProperty YaziProperty = BindableProperty.Create("Yazi", typeof(YaziModel),
            typeof(MenuYazilarOnizleme), default(YaziModel));
        public MenuYazilarOnizleme ()
		{
			InitializeComponent ();
            BindingContext = this;
		}
	}
}