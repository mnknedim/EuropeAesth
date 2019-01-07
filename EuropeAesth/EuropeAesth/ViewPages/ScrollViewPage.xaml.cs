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
            var strSubTitle = "Hava durumu çok bulutlu,yağışlar var. Uzmanlarımız bu hafta sonunun karla karışık ...";
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktor.png", Title = "Haberlerde bugün", SubTitle = strSubTitle });


            InitializeComponent();

            Lst.BindingContext = ScrollImages;
            Lst.ItemSelected += Lst_ItemSelected;

        }

        private async void Lst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedItem = (ImageScrollViewModel)e.SelectedItem;
            await Navigation.PushModalAsync(new ListViewDetail(SelectedItem));
        }
    }
}