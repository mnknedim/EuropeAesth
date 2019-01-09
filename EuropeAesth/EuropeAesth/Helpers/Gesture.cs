using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EuropeAesth.Helpers
{
    public static partial class Gesture
    {
        public static TapGestureRecognizer TappedGesture(EventHandler tapped)
        {
            var ret = new TapGestureRecognizer();
            ret.Tapped += tapped;
            return ret;
        }
    }
}
