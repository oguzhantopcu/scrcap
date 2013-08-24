using System.Drawing;

namespace EkranPaylas.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap Select(this Bitmap bitmap, Rectangle targetArea)
        {
            var selected = new Bitmap(targetArea.Width, targetArea.Height);

            using (var graphics = Graphics.FromImage(selected))
                graphics.DrawImage(bitmap, new Rectangle(0, 0, targetArea.Width, targetArea.Height),
                                   targetArea, GraphicsUnit.Pixel);

            return selected;
        }
    }
}
