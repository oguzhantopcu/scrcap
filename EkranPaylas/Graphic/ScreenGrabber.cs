using System;
using System.Drawing;
using System.Windows;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace EkranPaylas.Graphic
{
    public class ScreenGrabber : IScreenGrabber
    {
        public Bitmap Grab()
        {
            var size = new Size(Convert.ToInt32(SystemParameters.FullPrimaryScreenHeight),
                                Convert.ToInt32(SystemParameters.FullPrimaryScreenHeight));
            var bounds = new Rectangle(Point.Empty, size);

            var bitmap = new Bitmap(bounds.Width, bounds.Height);

            using (var gr = Graphics.FromImage(bitmap))
                gr.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

            return bitmap;
        }
    }
}
