using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Graphic;
using EkranPaylas.Messages;
using EkranPaylas.Uploaders.Infra;

namespace EkranPaylas.ViewModels
{
    public class ScreenGrabberViewModel : PropertyChangedBase , IHandle<ScreenGrabberState>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScreenGrabber _screenGrabber;
        private readonly IUploaderFactory _upladerFactory;
        private ScreenGrabberState _state;

        public IProgress<UploadResult> CurrentProgress { get; set; }

        public ScreenGrabberState State
        {
            get { return _state; }
            set
            {
                _state = value;

                _eventAggregator.Publish(State);

                NotifyOfPropertyChange(() => State);
            }
        }

        public BitmapImage BitmapImage { get; set; }
        
        public ScreenGrabberViewModel(IEventAggregator eventAggregator, IScreenGrabber screenGrabber, IUploaderFactory upladerFactory)
        {
            _eventAggregator = eventAggregator;
            _screenGrabber = screenGrabber;
            _upladerFactory = upladerFactory;

            _eventAggregator.Subscribe(this);
        }

        public void Handle(ScreenGrabberState message)
        {
            if (message == ScreenGrabberState.Select)
            {

                using (var source = _screenGrabber.Grab())
                {
                    var memoryStream = new MemoryStream();
                    source.Save(memoryStream, ImageFormat.Png);

                    BitmapImage = new BitmapImage();
                    BitmapImage.BeginInit();
                    BitmapImage.StreamSource = memoryStream;
                    BitmapImage.EndInit();
                }

                base.NotifyOfPropertyChange(() => BitmapImage);
            }
        }

        public void StartUpload()
        {
            var uploader = _upladerFactory.GetUploader(HostType.EkranPaylasHost);
            //BitmapImage.StreamSource.Read(BitmapImage.StreamSource.)
            //CurrentProgress = uploader.StartUpload(, Guid.NewGuid() + ".png");

            State = ScreenGrabberState.Upload;
        }

        public void CancelUpload()
        {
            if(CurrentProgress != null)
                CurrentProgress.Stop();

            State = ScreenGrabberState.Sleep;
        }

        public void CancelSelect()
        {
            State = ScreenGrabberState.Sleep;
        }

        public void Save()
        {
            
        }
    }
}