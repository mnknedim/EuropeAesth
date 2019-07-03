using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Interface
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YaziEkle : ContentPage
	{
		public YaziEkle ()
		{
			InitializeComponent ();
            var tabGest = new TapGestureRecognizer();
            tabGest.Tapped += ResimYukle_Tabbed;
            YaziResmi.GestureRecognizers.Add(tabGest);
		}

        MediaFile file;

        private async void ResimYukle_Tabbed(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                YaziResmi.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                
            }
            catch (Exception ex)
            {
               
            }
        }

        private async Task<string> StoreImages(Stream stream)
        {
            var stroageImage = await new FirebaseStorage("adjuvanclinic.appspot.com")
                .Child("AdjuvanImages")
                .Child("image.jpg")
                  .PutAsync(stream);
            string imgurl = stroageImage;
            return imgurl;
        }

        private async void Yayinla_Clicked(object sender, EventArgs e)
        {
            await StoreImages(file.GetStream());
        }
    }
}