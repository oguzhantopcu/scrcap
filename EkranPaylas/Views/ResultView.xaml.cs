using System.Windows;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Uploaders.Infra;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : IHandle<ScreenGrabberState>, IHandle<UploadResult>
    {
        public ResultView()
        {
            InitializeComponent();
        }

        public void Handle(ScreenGrabberState message)
        {
            if (message == ScreenGrabberState.UploadComplete)
                this.Show();

            Visibility = message == ScreenGrabberState.UploadComplete ? Visibility.Visible : Visibility.Collapsed;
        }

        public void Handle(UploadResult message)
        {
            
        }
    }
}
