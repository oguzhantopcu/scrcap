using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;

namespace EkranPaylas.ViewModels
{
    public class UploaderViewModel : PropertyChangedBase, IHandle<ScreenGrabberState>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        public UploaderViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;

            _eventAggregator.Subscribe(this);
        }

        public void Handle(ScreenGrabberState message)
        {
            //if (message == ScreenGrabberState.Sleep)
            //    _windowManager.ShowWindow(this, this);
        }

        public void Cancel()
        {
            _eventAggregator.Publish(ScreenGrabberState.Sleep);
        }
    }
}