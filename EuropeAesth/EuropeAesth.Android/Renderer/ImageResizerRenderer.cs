using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PMSPirelli.Droid.Renderers;
using PMSPirelli.Renderer;

using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResizerRenderer))]
namespace PMSPirelli.Droid.Renderers
{
    public class ImageResizerRenderer : IImageResizer
    {
        public ImageResizerRenderer() { }

        public Tuple<byte[], float, float> ResizeImage(byte[] imageData, float width, float height)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options()
                {
                    InPurgeable = true,
                };
                Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);
                return CreateImage(originalImage, width, height);
            }
            catch (System.Exception ex)
            {
                return Tuple.Create(imageData, width, height);
            }
        }

        public byte[] ResizeImage(string imagePath, float width, float height)
        {
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options()
                {
                    InPurgeable = true,
                };
                Bitmap originalImage = BitmapFactory.DecodeFile(imagePath, options);
                return CreateImage(originalImage, width, height).Item1;
            }
            catch (System.Exception ex)
            {
                return new byte[0];
            }
        }

        private Tuple<byte[], float, float>  CreateImage(Bitmap bitmap, float width, float height)
        {
            float newHeight = 0;
            float newWidth = 0;

            var originalHeight = bitmap.Height;
            var originalWidth = bitmap.Width;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                float ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                float ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(bitmap, (int)newWidth, (int)newHeight, true);
            bitmap.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                resizedImage.Recycle();
                return Tuple.Create(ms.ToArray(), newWidth, newHeight);
            }
        }

      
    }
}