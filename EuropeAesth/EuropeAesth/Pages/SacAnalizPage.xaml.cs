using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EuropeAesth.Annotations;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Messaging;
using PMSPirelli.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SacAnalizPage : ContentPage
    {
        public List<Attachment> imgStreams = new List<Attachment>();

        public SacAnalizPage ()
		{
			InitializeComponent ();
            var tabGest = new TapGestureRecognizer();
            tabGest.Tapped += ResimYukle_Tabbed;
            AnalizOn.GestureRecognizers.Add(tabGest);
            AnalizSag.GestureRecognizers.Add(tabGest);
            AnalizSol.GestureRecognizers.Add(tabGest);
		}

        MediaFile file;

        private async void ResimYukle_Tabbed(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(
                    new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    });
                if (file == null)
                    return;

                var imageStream = file.GetStream();

                byte[] OrgByte;
                using (BinaryReader br = new BinaryReader(imageStream))
                {
                    OrgByte = br.ReadBytes((int) imageStream.Length);
                }

                var resizedByte1000 = _resizer.ResizeImage(OrgByte, 1000, 1000).Item1;

                var imageSrc = (Image) sender;


                if (imageSrc.AutomationId == "On")
                {
                    AnalizOn.Source = ImageSource.FromStream(() => { return new MemoryStream(resizedByte1000); });
                    if (imgStreams.Any(x => x.Name == "OnTaraf.png"))
                        imgStreams.Remove(imgStreams.FirstOrDefault(x => x.Name == "OnTaraf.png"));

                    imgStreams.Add(new Attachment(new MemoryStream(resizedByte1000), "OnTaraf.png", "image/png"));
                }

                if (imageSrc.AutomationId == "Sag")
                {
                    AnalizSag.Source = ImageSource.FromStream(() => { return new MemoryStream(resizedByte1000); ; });
                    if (imgStreams.Any(x => x.Name == "SagTaraf.png"))
                        imgStreams.Remove(imgStreams.FirstOrDefault(x => x.Name == "SagTaraf.png"));

                    imgStreams.Add(new Attachment(new MemoryStream(resizedByte1000), "SagTaraf.png", "image/png"));
                }

                if (imageSrc.AutomationId == "Sol")
                {
                    AnalizSol.Source = ImageSource.FromStream(() => { return new MemoryStream(resizedByte1000); ; });
                    if (imgStreams.Any(x => x.Name == "SolTaraf.png"))
                        imgStreams.Remove(imgStreams.FirstOrDefault(x => x.Name == "SolTaraf.png"));

                    imgStreams.Add(new Attachment(new MemoryStream(resizedByte1000), "SolTaraf.png", "image/png"));
                }


            }
            catch (Exception ex)
            {

            }
        }

        static IImageResizer _resizer = DependencyService.Get<IImageResizer>();
        

        private void BtnSend_OnClicked(object sender, EventArgs e)
        {
             UserDialogs.Instance.ShowLoading("Gönderiliyor...",MaskType.None);
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(EntryMail.Text);
                mail.To.Add("info@adjuvanclinic.com");
                mail.To.Add("adjvanclinic@gmail.com");
                mail.Subject = "Mobile Saç Analizi";
                mail.Body = "Gönderen E-Mail :" + EntryMail.Text;
                foreach (var img in imgStreams)
                {
                    mail.Attachments.Add(new Attachment(img.ContentStream, img.Name,img.ContentType.MediaType));
                }
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("adjvanclinic@gmail.com", "Adjuvan.45");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
               
            }
           
            UserDialogs.Instance.HideLoading();
            DisplayAlert("Başarılı 😊", "", "Tamam");
        }


        public class AnalizResimler
        {
            public Stream str1 { get; set; }
            public string Url { get; set; }
            public string Name{ get; set; }
        }
    }
}