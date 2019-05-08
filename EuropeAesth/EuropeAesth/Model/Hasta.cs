using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class KullaniciHasta
    {
        public Guid Id { get; set; }
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
        public Guid HastaId { get; set; }
        public string TemsilciKod { get; set; }
        public string Hastahane { get; set; }
        public DateTime GirisTarih { get; set; }
        public DateTime CikisTarih { get; set; }
        public int GunSayisi { get; set; }
        public string Hotel { get; set; }
        public string Transfer { get; set; }
        public string VerilenTeklifEuro { get; set; }
        public string ToplamFiyatTl { get; set; }
        public string ToplamFiyatEuro { get; set; }
        public int OnayDurumu { get; set; }
        public string SonDurum { get; set; }
    }
}
