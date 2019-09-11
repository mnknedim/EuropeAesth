using Acr.UserDialogs;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.MasDetPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Page page;

            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            if (item.Title == "Profilim")
            {

            }
          
                page = (Page)Activator.CreateInstance(item.TargetType);
           
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        
    }
}