using System;
using System.Diagnostics;
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

            _eventAggregator.Subscribe(this);
        }

        private string _result;

        public void Handle(UploadResult message)
        {
            Result = message == null ? "" : message.Result;
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
            Clipboard.SetText(Result);
        }

        public void Open()
        {
            if (Uri.IsWellFormedUriString(Result, UriKind.Absolute))
                Process.Start(Result);
        }

        public void Cancel()
        {
            _eventAggregator.Publish(ScreenGrabberState.Sleep);
        }
    }
}