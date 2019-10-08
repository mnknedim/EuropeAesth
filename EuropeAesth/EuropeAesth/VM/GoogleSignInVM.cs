using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using EuropeAesth.Annotations;
using EuropeAesth.MasDetPage;
using EuropeAesth.Model;
using EuropeAesth.Pages;
using EuropeAesth.Pages.GoogleUser;
using EuropeAesth.Renderer;
using Firebase.Database;
using Firebase.Database.Query;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EuropeAesth.VM
{
    public class GoogleSignInVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private  IGoogleManager _googleManager;

        public Command GoogleLoginCommand { get; set; }
        public Command GoogleLogoutCommand { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private bool _isLogedIn;

        public bool IsLogedIn
        {
            get { return _isLogedIn; }
            set { _isLogedIn = value; }
        }

        private GoogleUser _googleUser;

        public GoogleUser GoogleUser
        {
            get { return _googleUser; }
            set { _googleUser = value; }
        }

        private string _topage;
        public string ToPage
        {
            get { return _topage; }
            set { _topage = value; }
        }

        public GoogleSignInVM()
        {
            IsLogedIn = false;
            GoogleLoginCommand = new Command(GoogleLogin);
            GoogleLogoutCommand = new Command(GoogleLogout);
        }

        private void GoogleLogout()
        {
            DependencyService.Get<IGoogleManager>().Logout();
            IsLogedIn = false;
            SecureStorage.Remove("GoogleLogin");
            App.Current.MainPage = new NavigationPage(new MainPage()) { BarTextColor = Color.FromHex("#304f72") };
        }

        private void GoogleLogin()
        {
           //   _googleManager.Login(OnLoginComplete);
          DependencyService.Get<IGoogleManager>().Login(OnLoginComplete);
        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
            try
            {
                if (googleUser != null)
                {
                    GoogleUser = googleUser;
                    IsLogedIn = true;
                    CheckGoogleUser(googleUser);
                    if (ToPage == "Anasayfa")
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new GUserPage(GoogleUser));
                    }
                    else
                    {
                       // PopupNavigation.Instance.PopAsync();
                    }

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", message, "Tamam");
                    await PopupNavigation.Instance.PopAsync();

                }
            }
            catch (Exception e)
            {
                
            }
            
        }
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        private async void CheckGoogleUser(GoogleUser user)
        {
           
                var allGoogleUser = await firebase.Child("GoogleUsers").OnceAsync<Model.GoogleUser>();
                var IsThereGUser = allGoogleUser?.Any(x => x.Object.Email == user.Email);
                await SecureStorage.SetAsync("GoogleLogin", user.Email);
                if (IsThereGUser == false)
                {
                    await firebase.Child("GoogleUsers").PostAsync(user);
                }

                App.Uyg.GoogleGirisYapan = user;

                await PopupNavigation.Instance.PopAsync();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
