using System.IO;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Graphic;

namespace EkranPaylas.ViewModels
{
    public class ScreenGrabberViewModel : IHandle<ScreenGrabMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IScreenGrabber _screenGrabber;

        public Stream BitmapStream { get; set; }

        public ScreenGrabberViewModel(IEventAggregator eventAggregator, IScreenGrabber screenGrabber)
        {
            _eventAggregator = eventAggregator;
            _screenGrabber = screenGrabber;
        }

        public void Handle(ScreenGrabMessage message)
        {
            _screenGrabber.Grab();
        }
    }
}
