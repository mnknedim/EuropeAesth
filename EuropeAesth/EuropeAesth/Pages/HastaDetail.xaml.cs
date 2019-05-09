using EuropeAesth.Custom;
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
        public HastaDetail (Hasta Hasta)
		{
			InitializeComponent ();
            st_Hasta.Children.Add(new HDLabel ("Ad Soyad : ", Hasta.KullaniciHasta.AdSoyad));
            st_Hasta.Children.Add(new HDLabel ("Email : ", Hasta.KullaniciHasta.Email));
            st_Hasta.Children.Add(new HDLabel ("Telefon : ", Hasta.KullaniciHasta.Telefon));
            st_Hasta.Children.Add(new HDLabel ("Ülke : " , Hasta.KullaniciHasta.Ulke));
            st_Hasta.Children.Add(new HDLabel ("Şehir : " , Hasta.KullaniciHasta.Şehir));

            var KHasta = Hasta.KayitliHasta;
            st_HastaIslem.Children.Add(new HDLabel ( "Hotel : " , KHasta.Hotel));
            st_HastaIslem.Children.Add(new HDLabel ( "Son Durum : " , KHasta.SonDurum));
            st_HastaIslem.Children.Add(new HDLabel ( "GirisTarih : " , KHasta.GirisTarih.ToString().Substring(0, KHasta.GirisTarih.ToString().IndexOf(" "))));
            st_HastaIslem.Children.Add(new HDLabel (  "CikisTarih : " , KHasta.CikisTarih.ToString().Substring(0, KHasta.CikisTarih.ToString().IndexOf(" "))));
            st_HastaIslem.Children.Add(new HDLabel ( "Kaç Gün : " , KHasta.GunSayisi.ToString()));
            st_HastaIslem.Children.Add(new HDLabel ( "Transfer : " , KHasta.Transfer ));
            st_HastaIslem.Children.Add(new HDLabel ( "Toplam ₺ : " , KHasta.ToplamFiyatTl + " ₺"));
            st_HastaIslem.Children.Add(new HDLabel ("Toplam € : " , KHasta.ToplamFiyatEuro +  " €"));
            st_HastaIslem.Children.Add(new HDLabel ("Teklif € : " , KHasta.VerilenTeklifEuro + "€"));


        }
    }
}