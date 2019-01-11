using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Component
{
    public class BaslikLabel : Label
    {
        public BaslikLabel()
        {
            Margin = new Thickness(10, 20);
            VerticalTextAlignment = TextAlignment.Center;
            HorizontalTextAlignment = TextAlignment.Start;
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            FontAttributes = FontAttributes.Bold;
        }
    }
}
