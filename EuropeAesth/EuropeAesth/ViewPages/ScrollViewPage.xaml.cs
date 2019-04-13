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

            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "sacekimi.png", Title = "Saç ekimi", SubTitle = "Saç ekimi hakkında" });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "fueyontemi.png", Title = "Fue Yöntemi", SubTitle = "Fue yöntemi nedir?" });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "prpdestekli.png", Title = "Prp Destekli Saç Ekimi", SubTitle = "Prp Destekli Saç Ekimi nedir?" });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "bilinmesigerekenler.png", Title = "Bilinmesi gerekenler", SubTitle = "Bilinmesi gerekenler" });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "operasyonsuresi.png", Title = "Operasyon süresi", SubTitle = "Operasyon süresi ne kadardır?" });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "faydazarar.png", Title = "Yarar ve zarar", SubTitle = "Saç ekiminin fayda ve zararlar" });
            ScrollImages.Add(new ImageScrollViewModel { ImageUrl = "doktorlar.png", Title = "Doktorlar", SubTitle = "Doktorlar hakkında" });


            InitializeComponent();

            Lst.BindingContext = ScrollImages;
            Lst.ItemSelected += Lst_ItemSelected;

        }

        private async void Lst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var SelectedItem = (ImageScrollViewModel)e.SelectedItem;
            var page = new ListViewDetail(SelectedItem);
            NavigationPage.SetHasBackButton(page,true);
            await Navigation.PushModalAsync(new NavigationPage(page),true);
        }
    }
}