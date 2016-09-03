using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bosxixi.Image
{
    public static class ImageProcessing
    {
        public static bool ChangeResolutionAnSave(string inputFilename, string outputFilename, Size size)
        {
            try
            {
                var bitmap = new Bitmap(inputFilename);
                var newBitmap = new Bitmap(bitmap, size);
                newBitmap.Save(outputFilename, System.Drawing.Imaging.ImageFormat.Jpeg);

                bitmap.Dispose();
                newBitmap.Dispose();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
