using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EuropeAesth.Custom;
using EuropeAesth.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Xamarin.Forms.ListView;

[assembly:ExportRenderer(typeof(ExListView), typeof(CustomListView))]
namespace EuropeAesth.Droid.Renderer
{
    public class CustomListView : ListViewRenderer
    {
         Context _context;
         ExListView element;

        public CustomListView(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            element = (ExListView)this.Element;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {

            if (element.IsScrollEnable)
            {
                return base.OnInterceptTouchEvent(ev);
            }
            else
            {
                ev.Action = MotionEventActions.Cancel;
                return base.OnInterceptTouchEvent(ev);
            }

        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (element.IsScrollEnable)
            {
                return base.OnTouchEvent(e);
            }
            else
            {
                return false;
            }
        }
    }
}