using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class YoneticiModel
    {
        public string YoneticiKod { get; set; }
        public int YetkiKod { get; set; }
        public string Parola { get; set; }
        public string YoneticiAd { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }

    }

    public class AllUser
    {
        public string UserKod { get; set; }
        public int YetkiKod { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string AdSoyad { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Telefon { get; set; }
    }
}
