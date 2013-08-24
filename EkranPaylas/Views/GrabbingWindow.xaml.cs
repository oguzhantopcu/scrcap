using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using EkranPaylas.Graphic;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for GrabbingWindow.xaml
    /// </summary>
    public partial class GrabbingWindow : Window
    {
        public GrabbingWindow()
        {
            var model = new ScreenGrabber();
            var grabber = new ScreenGrabberViewModel(null, new ScreenGrabber());
            grabber.BitmapStream = new MemoryStream();
            model.Grab().Save(grabber.BitmapStream, ImageFormat.Jpeg);
            this.DataContext = grabber;

            InitializeComponent();

        }
    }
}
