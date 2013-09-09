using System;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Core;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : IHandle<ScreenGrabberState>
    {
        private readonly IEventAggregator _eventAggregator;

        public ResultView()
        {
            InitializeComponent();

            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            _eventAggregator.Subscribe(this);

            if (string.IsNullOrEmpty(ServiceLocator.Current.GetInstance<ResultViewModel>().Result))
                Title = "Error!";
        }

        public void Handle(ScreenGrabberState message)
        {
            if(message == ScreenGrabberState.Sleep)
                Close();
        }

        public new ResultViewModel DataContext { get { return base.DataContext as ResultViewModel; } }

        protected override void OnClosed(EventArgs e)
        {
            this.DataContext.Cancel();

            _eventAggregator.Unsubscribe(this);

            base.OnClosed(e);
        }
    }
}
