using EuropeAesth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.ViewDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VideoView : ContentPage
	{
        public VideoModel SecVideo
        {
            get { return (VideoModel)GetValue(SecVideoProperty); }
            set { SetValue(SecVideoProperty, value); }
        }
        public static readonly BindableProperty SecVideoProperty =
            BindableProperty.Create(nameof(SecVideo), typeof(VideoModel), typeof(VideoView), default(VideoModel));

        public bool FullScreen
        {
            get => (bool)GetValue(FullScreenProperty);
            set => SetValue(FullScreenProperty, value);
        }
        public static readonly BindableProperty FullScreenProperty = BindableProperty.Create("FullScreen", typeof(bool), typeof(VideoView), true);
        public VideoView ()
		{
			InitializeComponent ();
            BindingContext = this;
            

        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width > height)
            {
                videoPlayer.VerticalOptions = LayoutOptions.FillAndExpand;
                videoPlayer.HorizontalOptions = LayoutOptions.FillAndExpand;
                DetayStack.IsVisible = false;
                FullScreen = false;

            }
            else
            {
                FullScreen = true;
                DetayStack.IsVisible = true;
                videoPlayer.VerticalOptions = LayoutOptions.Start;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == SecVideoProperty.PropertyName)
            {
                videoPlayer.Source = SecVideo.VideoUrl;
            }
        }

    }
}