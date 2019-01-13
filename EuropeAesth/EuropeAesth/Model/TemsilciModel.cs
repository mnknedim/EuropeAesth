using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class TemsilciModel
    {
        public string TemsilciKod { get; set; }
        public int YetkiKod { get; set; }
        public string Parola { get; set; }
        public string TemsilciAd { get; set; }
        public string AdSoyad { get; set; }
        public string Ulke { get; set; }
        public string Sehir { get; set; }
        public string Telefon { get; set; }
        public int UstTemsilci { get; set; }
        public List<int> AltTemsilci { get; set; }



    }
}
