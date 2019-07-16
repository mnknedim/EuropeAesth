using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuropeAesth.Pages.Interface
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Videolar : ContentPage
	{
		public Videolar ()
		{
			InitializeComponent ();
            var youTube = YouTube.Default; // starting point for YouTube actions
            var video = youTube.GetVideo("https://www.youtube.com/watch?v=U1p6CMFbFNc&t=6s");
            var octPlayer = new Octane.Xamarin.Forms.VideoPlayer.VideoPlayer(video.GetUri());
            videostack.Children.Add(octPlayer);
            videostack.HorizontalOptions = LayoutOptions.FillAndExpand;
            videostack.VerticalOptions = LayoutOptions.FillAndExpand;
            videostack.BackgroundColor = Color.Aqua;

        }
	}
}