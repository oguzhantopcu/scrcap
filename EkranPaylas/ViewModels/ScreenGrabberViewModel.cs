using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Invocation;
using EkranPaylas.Core;
using EkranPaylas.Extensions;
using EkranPaylas.Graphic;
using EkranPaylas.Uploaders.Infra;
using EkranPaylas.Utilities;

namespace EkranPaylas.ViewModels
{
    public class ScreenGrabberViewModel : PropertyChangedBase, IHandle<ScreenGrabberState>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScreenGrabber _screenGrabber;
        private readonly IUploaderFactory _upladerFactory;
        private readonly IWindowManager _windowManager;
        private readonly IDispatcher _dispatcher;
        private readonly IStateHolder _stateHolder;

        public IProgress<UploadResult> CurrentProgress { get; set; }

        public ScreenGrabberState State
        {
            get { return _stateHolder.State; }
        }

        public BitmapImage BitmapImage { get; set; }

        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public ScreenGrabberViewModel(IEventAggregator eventAggregator, IScreenGrabber screenGrabber,
            IUploaderFactory upladerFactory, IWindowManager windowManager, IDispatcher dispatcher,
            IStateHolder stateHolder)
        {
            _eventAggregator = eventAggregator;
            _screenGrabber = screenGrabber;
            _upladerFactory = upladerFactory;
            _windowManager = windowManager;
            _dispatcher = dispatcher;
            _stateHolder = stateHolder;

            _eventAggregator.Subscribe(this);
        }

        public void Handle(ScreenGrabberState message)
        {
            if (message == ScreenGrabberState.Sleep && CurrentProgress != null)
            {
                CurrentProgress.Stop();
            }

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
                Left = Top = -1;
                Height = Width = 1;

                base.NotifyOfPropertyChange(() => BitmapImage);
            }
        }

        public void StartUpload()
        {
            var uploader = _upladerFactory.GetUploader(HostType.EkranPaylasHost);

            CurrentProgress = uploader.StartUpload(SelectImage().GetBuffer(), Guid.NewGuid() + ".png");
            CurrentProgress.Completed += result => _dispatcher.ExecuteOnUIThread(() =>
            {
                _windowManager.ShowWindow(ServiceLocator.Current.GetInstance<ResultViewModel>(), null);

                _eventAggregator.Publish(ScreenGrabberState.UploadComplete);
                _eventAggregator.Publish(result);
            });

            _windowManager.ShowWindow(ServiceLocator.Current.GetInstance<UploaderViewModel>(), null);

            _eventAggregator.Publish(ScreenGrabberState.Upload);
        }

        public void Copy()
        {
            System.Windows.Forms.Clipboard.SetImage(SelectImage());

            Cancel();
        }

        public void Cancel()
        {
            _eventAggregator.Publish(ScreenGrabberState.Sleep);
        }

        protected Image SelectImage()
        {
            return BitmapImage
                .Select()
                .Select(Convert.ToInt32(Left), Convert.ToInt32(Top), Convert.ToInt32(Width), Convert.ToInt32(Height));
        }

        public void Save()
        {
            using(var dialog = new SaveFileDialog())
                if (DialogResult.OK == dialog.ShowDialog())
                    Save(dialog.FileName);
        }

        public void Save(string location)
        {
            File.Delete(location);

            SelectImage().Save(location, ImageFormat.Png);

            Cancel();
        }
    }
}