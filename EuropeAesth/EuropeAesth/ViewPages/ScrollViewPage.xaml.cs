using EuropeAesth.Component;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.ViewPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScrollViewPage : ContentView
    {


        ObservableCollection<ImageScrollViewModel> ScrollImages = new ObservableCollection<ImageScrollViewModel>();

        public ScrollViewPage()
        {
            var Y = new Yazilar();
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "sacekimi.png", Title = "Saç Ekimi", SubTitle = Y.DYazilar["sacekimi"].Substring(0,100) });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "fueyontemi.png", Title = "Fue Yöntemi", SubTitle = Y.DYazilar["fueyontemi"].Substring(0, 100) });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "temelasamalar.png", Title = "Temel Aşamalar", SubTitle = Y.DYazilar["temelasamalar"].Substring(0, 100) });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "turkiyesacekimi.png", Title = "Türkiyede Saç Ekimi", SubTitle = Y.DYazilar["turkiyesacekimi"].Substring(0, 100) });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "trassizsacekimi.png", Title = "Tıraşsız Saç Ekimi", SubTitle = Y.DYazilar["trassizsacekimi"].Substring(0, 100) });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "ekimoperasyon.png", Title = "Ekim Operasyonu", SubTitle = Y.DYazilar["ekimoperasyon"].Substring(0, 100) });


            InitializeComponent();

            Lst.BindingContext = ScrollImages;
            Lst.ItemSelected += Lst_ItemSelected;

        }

        private void Lst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedItem = (ImageScrollViewModel)e.SelectedItem;
            App.Current.MainPage = new ListViewDetail(SelectedItem);
        }
    }
}