using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EkranPaylas.Extensions
{
    public static class ImageExtensions
    {
        public static Image Select(this Image originalImage, int left, int top, int width, int height)
        {
            var image = new Bitmap(width, height);
            using (var processer = Graphics.FromImage(image))
                processer.DrawImage(originalImage, 0, 0, new Rectangle(left, top, width, height), GraphicsUnit.Pixel);

            return image;
        }

        public static byte[] GetBuffer(this Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();
            }
        }
    }
}