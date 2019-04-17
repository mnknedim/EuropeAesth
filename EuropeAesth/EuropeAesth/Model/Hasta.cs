using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class KullaniciHasta
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string AdSoyad { get; set; }
        public string Parola { get; set; }
        public int YetkiKod { get; set; }
        public string Telefon { get; set; }
        public string PromosyonKod { get; set; }
        public string Ulke { get; set; }
        public string Şehir { get; set; }
        public string TemsilciKod { get; set; }
    }

    public class KayitliHasta
    {
        public string HastaKod { get; set; }
        public string TemsilciKod { get; set; }
        public string Hastahane { get; set; }
        public string Hotel { get; set; }
        public string Transfer { get; set; }
        public string VerilenTeklif { get; set; }
        public string ToplamFiyat { get; set; }
        public int OnayDurumu { get; set; }
        public string SonDurum { get; set; }
    }
}
