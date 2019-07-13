using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Foundation;
using MediaSample.iOS.Renderers;
using PMSPirelli.Renderer;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResizerRenderer))]
namespace MediaSample.iOS.Renderers
{
    class ImageResizerRenderer : IImageResizer
    {
        public Tuple<byte[], float, float> ResizeImage(byte[] imageData, float width, float height)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            return CreateImage(originalImage, width, height);
        }

        public Tuple<byte[], float, float> ResizeImage(string imagePath, float width, float height)
        {
            UIImage originalImage = ImageFormPath(imagePath);
            return CreateImage(originalImage, width, height);
        }

        private UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
                return null;

            return new UIImage(Foundation.NSData.FromArray(data));
        }

        private UIImage ImageFormPath(string path)
        {
            if (String.IsNullOrEmpty(path))
                return null;

            return new UIImage(path);
        }

        private Tuple<byte[], float, float> CreateImage(UIImage image, float width, float height)
        {
            var originalHeight = image.Size.Height;
            var originalWidth = image.Size.Width;

            nfloat newHeight = 0;
            nfloat newWidth = 0;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                nfloat ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                nfloat ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            width = (float)newWidth;
            height = (float)newHeight;

            UIGraphics.BeginImageContext(new SizeF(width, height));
            image.Draw(new RectangleF(0, 0, width, height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            var bytesImagen = resizedImage.AsJPEG().ToArray();
            resizedImage.Dispose();
            return Tuple.Create(bytesImagen,width,height);
        }
    }
}