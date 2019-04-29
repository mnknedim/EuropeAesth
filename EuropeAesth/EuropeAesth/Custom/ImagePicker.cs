using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PMSPirelli.Custom
{
    public class ImagePicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(ImagePicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
