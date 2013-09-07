using System.Windows;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace EkranPaylas.Extensions
{
    public static class BitmapImageExtensions
    {
        public static System.Drawing.Image Select(this BitmapImage originalImage)
        {
            var imageControl = new Image {Source = originalImage};

            var size = new Size(originalImage.PixelWidth, originalImage.PixelHeight);
            imageControl.Measure(size);
            var rect = new Rect(new Point(0, 0), size);
            imageControl.Arrange(rect);

            return imageControl.TakeImage(96, 96);
        }
    }
}