using System.Windows.Forms;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class NotifyIconStartupTask : IStartupTask
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;
        private readonly ScreenGrabberViewModel _screenGrabberViewModel;

        public NotifyIconStartupTask(MainViewModel mainViewModel, IEventAggregator eventAggregator, IWindowManager windowManager, ScreenGrabberViewModel screenGrabberViewModel)
        {
            _mainViewModel = mainViewModel;
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _screenGrabberViewModel = screenGrabberViewModel;
        }

        public int Priority { get; private set; }

        public void Select()
        {
            _eventAggregator.Publish(ScreenGrabberState.Select);
            _windowManager.ShowWindow(_screenGrabberViewModel);
        }

        public void Execute()
        {
            var notifyIcon = new NotifyIcon
            {
                BalloonTipText = "Hey! Ekranpaylas is here!",
                Text = "EkranPaylas",
                Icon = new System.Drawing.Icon("cloud_off.ico"),
                Visible = true,
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Select", (sender, args) => Select()),
                    new MenuItem("Exit", (sender, args) => _mainViewModel.Exit())
                }),
            };
            notifyIcon.Click += (sender, args) => Select();
            notifyIcon.ShowBalloonTip(1000);
        }
    }
}