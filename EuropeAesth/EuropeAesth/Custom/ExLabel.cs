using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Custom
{
    public class ExLabel : Label
    {
        public ExLabel()
        {
            FontAttributes = FontAttributes.Bold;
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            Margin = new Thickness(20);
        }
    }
}
