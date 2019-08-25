using Android.Content;
using Android.Support.V7.Graphics.Drawable;
using Android.Widget;
using EuropeAesth.Droid.Renderers;
using EuropeAesth.MasDetPage;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(MainPage), typeof(MasterDetailRenderer))]
namespace EuropeAesth.Droid.Renderers
{
    public class MasterDetailRenderer : MasterDetailPageRenderer
    {
        Context _context;
        public MasterDetailRenderer(Context contex) : base(contex)
        {
            _context = contex;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            if (toolbar != null)
            {
                toolbar.SetTitleTextColor(Resource.Color.colorMyBarTitle);

                for (var i = 0; i < toolbar.ChildCount; i++)
                {
                    var imageButton = toolbar.GetChildAt(i) as Android.Widget.ImageButton;

                    var imageInd = toolbar.GetChildAt(i) as Android.Widget.ImageButton;

                    var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
                    if (drawerArrow == null)
                        continue;

                    imageButton.SetImageDrawable(_context.GetDrawable(Resource.Drawable.ic_adj_actionBar));

                }
            }
        }

        //private void SetNavigationIcon(Context context, Bitmap dstBitmap)
        //{
        //    var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
        //    var navIcon = toolbar.NavigationIcon.Callback as Android.Widget.ImageButton;

        //    navIcon?.SetImageBitmap(dstBitmap);
        //}
    }
}