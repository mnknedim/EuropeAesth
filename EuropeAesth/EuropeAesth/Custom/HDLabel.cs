using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Custom
{
    public class HDLabel : Label
    {
        public HDLabel(string key, string value)
        {
            var formString = new FormattedString();
            formString.Spans.Add(
                new Span {
                    Text = key,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    FontAttributes = FontAttributes.Bold,

                 });

            formString.Spans.Add(
               new Span
               {
                   Text = value,
                   FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
               });

            FormattedText = formString;

            Margin = new Thickness(10);
        }
    }
}
