using System;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Core;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UploaderView : IHandle<ScreenGrabberState>
    {
        private readonly IEventAggregator _eventAggregator;

        public UploaderView()
        {
            InitializeComponent();

            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            _eventAggregator.Subscribe(this);
        }

        public void Handle(ScreenGrabberState message)
        {
            this.CurrentState = message;

            if(message == ScreenGrabberState.Sleep || message == ScreenGrabberState.UploadComplete)
                Close();

            Left = 20;
            Top = 20;
        }

        public ScreenGrabberState CurrentState { get; set; }
        public new UploaderViewModel DataContext { get { return base.DataContext as UploaderViewModel; } }

        protected override void OnClosed(EventArgs e)
        {
            if (CurrentState != ScreenGrabberState.UploadComplete)
                this.DataContext.Cancel();
            _eventAggregator.Unsubscribe(this);

            base.OnClosed(e);
        }
    }
}
