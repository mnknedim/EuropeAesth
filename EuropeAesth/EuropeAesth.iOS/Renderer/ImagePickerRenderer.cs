using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PMSPirelli.Custom;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace EuropeAesth.iOS.Renderer
{
    public class ImagePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var element = (ImagePicker)this.Element;

            if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {
                var downarrow = UIImage.FromBundle(element.Image);
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.LeftView = new UIImageView(downarrow);
            }
        }
    }
}