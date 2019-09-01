using Acr.UserDialogs;
using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using EuropeAesth.Pages.Interface;
using EuropeAesth.Pages.MenuPages;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.MasDetPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;


        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;

            var webTab = new TapGestureRecognizer();
            webTab.Tapped += WebButon_Clicked;
            WebButon.GestureRecognizers.Add(webTab);

            var instaTab = new TapGestureRecognizer();
            instaTab.Tapped += InstaButon_Clicked;
            InstaButon.GestureRecognizers.Add(instaTab);

            var twtTab = new TapGestureRecognizer();
            twtTab.Tapped += TwitterButon_Clicked;
            TwitterButon.GestureRecognizers.Add(twtTab);
        }


        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }
           FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");

            public MainPageMasterViewModel()
            {
                MainPageMenuItem banaOzelPage = null;


                Task.Run(async () => {
                    banaOzelPage = await CheckLogined(); });


                MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
                { 
                    new MainPageMenuItem { Id = 0, Title = "Anasayfa", Icon = "ic_dashboard.png", TargetType= typeof(TabbedMainPage) },
                    new MainPageMenuItem { Id = 1, Title = "Yazilar", Icon = "ic_yazilar.png", TargetType= typeof(MenuYazilar) },
                    new MainPageMenuItem { Id = 2, Title = "Videolar", Icon="ic_videolar.png", TargetType= typeof(MenuVideolar)  },
                    new MainPageMenuItem { Id = 3, Title = "Resimler" , Icon = "ic_resimler.png"},
                    new MainPageMenuItem { Id = 4, Title = "Hakkımızda" , Icon = "ic_hakkimizda.png", TargetType= typeof(Hakkimizda)},
                    banaOzelPage
                });
            }

            private async Task<MainPageMenuItem> CheckLogined()
            {
                MainPageMenuItem page = null;
                try
                {
                    var userName = await SecureStorage.GetAsync("UserKod");
                    var passWord = await SecureStorage.GetAsync("Parola");

                    if (userName == null)
                        return new MainPageMenuItem
                        {
                            Id = 5,
                            Title = "Bana Özel",
                            Icon = "ic_user.png",
                            TargetType = typeof(BanaOzel)
                        };

                    UserDialogs.Instance.ShowLoading("Lütfen Bekleyiniz..", MaskType.Black);

                    var userResult = await firebase.Child("AllUser").OnceAsync<AllUser>();
                    if (userResult != null)
                    {
                        var user = userResult.FirstOrDefault(x => x.Object.UserKod == userName && x.Object.Parola == passWord).Object;
                        if (user == null)
                            page = new MainPageMenuItem
                            {
                                Id = 5,
                                Title = "Bana Özel",
                                Icon = "ic_user.png",
                                TargetType = typeof(BanaOzel)
                            };

                        App.Uyg.LoginUser = user;

                        if (user.YetkiKod == 1)
                            page = new MainPageMenuItem
                            {
                                Id = 5,
                                Title = "Bana Özel",
                                Icon = "ic_user.png",
                                TargetType = typeof(YoneticiPage)
                            };

                        if (user.YetkiKod == 2)
                            page = new MainPageMenuItem
                            {
                                Id = 5,
                                Title = "Bana Özel",
                                Icon = "ic_user.png",
                                TargetType = typeof(TemsilciPage)
                            };
                    }
                }
                catch (Exception ex)
                {
                    // Possible that device doesn't support secure storage on device.
                }

                return page;
            }



            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void WebButon_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://adjuvanclinic.com/"));
        }

        private void InstaButon_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/adjuvanclinic/"));
        
        }

        private void TwitterButon_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://twitter.com/adjuvanclinic"));
        }
    }
}