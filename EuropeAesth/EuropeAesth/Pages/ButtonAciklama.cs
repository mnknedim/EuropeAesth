using EuropeAesth.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EuropeAesth.Pages
{
	public class ButtonAciklama : ContentPage
	{
		public ButtonAciklama ()
		{
            var NewGrid = new ExGrid();
            NewGrid.Children.Add(new Image { Source = "arkplan.png" ,
                Aspect = Aspect.AspectFill, 
            },0,0);
            NewGrid.Children.Add(new Image { Source = "LogoHastaneBilgileri.png",
                VerticalOptions = LayoutOptions.Start,
                
                HorizontalOptions = LayoutOptions.CenterAndExpand

            }, 0,0);

            Content = new StackLayout {
				Children = {
                    NewGrid
                }
			};
		}
	}
}