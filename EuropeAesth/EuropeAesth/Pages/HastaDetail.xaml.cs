using EuropeAesth.Model;
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
	public partial class HastaDetail : ContentPage
	{
        //public KullaniciHasta KullaniciHasta
        //{
        //    get { return (KullaniciHasta)GetValue(KullaniciHastaProperty); }
        //    set { SetValue(KullaniciHastaProperty, value); }
        //}
        //public static readonly BindableProperty KullaniciHastaProperty = BindableProperty.Create("KullaniciHasta", typeof(KullaniciHasta), typeof(HastaDetail), default(KullaniciHasta));
        public HastaDetail (KullaniciHasta KullaniciHasta)
		{
			InitializeComponent ();
            st_Hasta.Children.Add(new Label { Text = KullaniciHasta.AdSoyad, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) });
            st_Hasta.Children.Add(new Label { Text = KullaniciHasta.Email, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) });
            st_Hasta.Children.Add(new Label { Text = KullaniciHasta.Telefon, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) });
            st_Hasta.Children.Add(new Label { Text = KullaniciHasta.Ulke, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) });
            st_Hasta.Children.Add(new Label { Text = KullaniciHasta.Şehir, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) });
		}
	}
}