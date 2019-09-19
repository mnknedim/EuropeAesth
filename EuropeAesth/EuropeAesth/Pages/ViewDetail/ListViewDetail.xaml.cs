using EuropeAesth.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewDetail : ContentPage
	{
        public YaziModel SecYazi
        {
            get { return (YaziModel)GetValue(SecYaziProperty); }
            set { SetValue(SecYaziProperty, value); }
        }
        public static readonly BindableProperty SecYaziProperty =
            BindableProperty.Create(nameof(SecYazi), typeof(YaziModel), typeof(ListViewDetail), default(YaziModel));

        public ObservableCollection<YorumlarModel> Yorumlar
        {
            get { return (ObservableCollection<YorumlarModel>)GetValue(YorumlarProperty); }
            set { SetValue(YorumlarProperty, value); }
        }
        public static readonly BindableProperty YorumlarProperty =
            BindableProperty.Create(nameof(Yorumlar), typeof(ObservableCollection<YorumlarModel>), typeof(ListViewDetail), default(ObservableCollection<YorumlarModel>));

        DisplayInfo displayInfo;
        FirebaseClient firebase = new FirebaseClient("https://adjuvanclinic.firebaseio.com/");
        public ListViewDetail ()
		{
			InitializeComponent ();
            displayInfo = DeviceDisplay.MainDisplayInfo;
            YaziResim.HeightRequest = displayInfo.Height / 10;
            BindingContext = this;

            LoadYorumlar();
        }

        private async void LoadYorumlar()
        {
            var response = await firebase.Child("Yorumlar").OnceAsync<YorumlarModel>();
            var result = response.Where(x => x.Object.YaziId == SecYazi.Id);
            List<YorumlarModel> yorumList = new List<YorumlarModel>();

            foreach (var item in result)
                Yorumlar.Add(item.Object);

            //YorumlarList.ItemsSource = Yorumlar;
            YorumlarList.BindingContext = Yorumlar;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var dateTime = DateTime.Now.ToString("dd.MM.yyyy");
                var yorum = new YorumlarModel
                {
                    UserName = "nedim",
                    YorumText = YorumEditor.Text,
                    YaziId = SecYazi.Id,
                    DateTime = dateTime
                };

                await firebase.Child("Yorumlar").PostAsync(yorum);

            }
            catch (Exception ex)
            {

                
            }
            

        }
    }
}