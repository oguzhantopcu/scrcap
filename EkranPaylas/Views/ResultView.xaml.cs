using System;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Core;
using EkranPaylas.Uploaders.Infra;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : IHandle<ScreenGrabberState>, IHandle<UploadResult>
    {
        private readonly IEventAggregator _eventAggregator;

        public ResultView()
        {
            InitializeComponent();

            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            _eventAggregator.Subscribe(this);

            SetTitle();
        }

        public void SetTitle()
        {
            Title = string.IsNullOrEmpty(ServiceLocator.Current.GetInstance<ResultViewModel>().Result) ? "Hata!" : "Başarılı";
        }

        public void Handle(ScreenGrabberState message)
        {
            SetTitle();

            if (message == ScreenGrabberState.Sleep)
                Close();
        }

        public new ResultViewModel DataContext { get { return base.DataContext as ResultViewModel; } }

        protected override void OnClosed(EventArgs e)
        {
            this.DataContext.Cancel();

            _eventAggregator.Unsubscribe(this);

            base.OnClosed(e);
        }

        public void Handle(UploadResult message)
        {
            SetTitle();
        }
    }
}
