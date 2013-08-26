using System.Windows.Media.Imaging;
using EkranPaylas.Messages;
using EkranPaylas.Uploaders.Infra;

namespace EkranPaylas.ViewModels
{
    public interface IScreenGrabberViewModel
    {
        IProgress<UploadResult> CurrentProgress { get; set; }
        ScreenGrabberState State { get; set; }
        BitmapImage BitmapImage { get; set; }
        void StartUpload();
        void CancelUpload();
        void CancelSelect();
        void Save();
        void Handle(ScreenGrabMessage screenGrabMessage);
    }
}