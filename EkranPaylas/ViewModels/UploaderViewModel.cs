using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;

namespace EkranPaylas.ViewModels
{
    public class UploaderViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        public UploaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Cancel()
        {
            _eventAggregator.Publish(ScreenGrabberState.Sleep);
        }
    }
}