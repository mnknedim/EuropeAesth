﻿using EuropeAesth.Model;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Temsilci
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hastalarim : TabbedPage
    {
        public Hastalarim ()
        {
            InitializeComponent();
            FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
            var TumHastalar = firebase.Child("KayitliHasta").OnceAsync<KayitliHasta>().Result;
            var bekleyenHastalar = TumHastalar.Where(x => x.Object.OnayDurumu == 0 && x.Object.TemsilciKod == App.Uyg.LoginUser.UserKod);
            var onaylananHastalar = TumHastalar.Where(x => x.Object.OnayDurumu == 1 && x.Object.TemsilciKod == App.Uyg.LoginUser.UserKod);
            var taburcuHastalar = TumHastalar.Where(x => x.Object.OnayDurumu == 2 && x.Object.TemsilciKod == App.Uyg.LoginUser.UserKod);
            BackgroundColor = Color.FromHex("#02b294");
            Children.Add(new OnaylananHastalar(onaylananHastalar) { Title = "Onaylanan" });
            Children.Add(new BekleyenHastalar(bekleyenHastalar) { Title = "Bekleyen" });
            Children.Add(new TaburcuHastalar(taburcuHastalar) { Title = "Taburcu" });

        }
    }
}