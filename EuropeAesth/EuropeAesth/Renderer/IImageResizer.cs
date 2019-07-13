using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PMSPirelli.Renderer
{
    public interface IImageResizer
    {
        Tuple<byte[], float, float> ResizeImage(byte[] imageData, float width, float height);
        //Tuple<byte[], float, float> ResizeImage(string imagePath, float width, float height);
    }
}
