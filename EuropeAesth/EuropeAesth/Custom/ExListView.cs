using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Custom
{
    public class ExListView:ListView
    {
        public bool IsScrollEnable
        {
            get { return (bool)GetValue(IsScrollEnableProperty); }
            set { SetValue(IsScrollEnableProperty, false); }
        }
        public static readonly BindableProperty IsScrollEnableProperty = BindableProperty.Create(nameof(IsScrollEnable), typeof(bool), typeof(ExListView), default(bool));

        public ExListView()
        {
            
        }
      

    }
}
