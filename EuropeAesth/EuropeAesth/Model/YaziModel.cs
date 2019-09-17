﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Model
{
    public class YaziModel
    {
        public string Id { get; set; }
        public string SelectedId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string KisaAciklama { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Tarih { get; set; }
    }

    public class VideoModel
    {
        public string Id { get; set; }
        public string VideoUrl { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string KisaAciklama { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Tarih { get; set; }
    }
}
