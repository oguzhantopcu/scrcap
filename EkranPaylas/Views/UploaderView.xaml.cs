using System.Windows;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UploaderView : IHandle<ScreenGrabberState>
    {
        public UploaderView()
        {
            InitializeComponent();
        }

        public void Handle(ScreenGrabberState message)
        {
            if (message == ScreenGrabberState.Upload)
                this.Show();

            this.Visibility = message == ScreenGrabberState.Upload ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
