using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Component
{
    public class YanLabel : Label
    {
        public YanLabel()
        {
            Margin = new Thickness(10, 20);
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalTextAlignment = TextAlignment.Center;
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            FontAttributes = FontAttributes.None;
        }
    }
}
