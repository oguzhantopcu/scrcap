using System.Windows;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Uploaders.Infra;

namespace EkranPaylas.ViewModels
{
    public class ResultViewModel : PropertyChangedBase, IHandle<UploadResult>
    {
        private readonly IEventAggregator _eventAggregator;

        public ResultViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private string _result;

        public void Handle(UploadResult message)
        {
            this.Result = message.Result;
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;

                NotifyOfPropertyChange(() => Result);
            }
        }

        public void Copy()
        {
            Clipboard.SetData(DataFormats.StringFormat, Result);
        }

        public void Close()
        {
            _eventAggregator.Publish(ScreenGrabberState.Sleep);
        }
    }
}