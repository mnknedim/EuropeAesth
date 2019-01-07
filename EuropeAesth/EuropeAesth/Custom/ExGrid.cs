using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Custom
{
    public class ExGrid : Grid
    {
        public ExGrid()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;

            for (int i = 0; i < 1; i++)
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            for (int i = 0; i < 1; i++)
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }
    }
}
