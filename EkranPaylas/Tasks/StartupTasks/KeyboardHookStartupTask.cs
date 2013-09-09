using System.Windows.Forms;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Invocation;
using EkranPaylas.Core;
using EkranPaylas.Utilities;
using EkranPaylas.ViewModels;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class KeyboardHookStartupTask : IStartupTask
    {
        private readonly IStateHolder _stateHolder;
        private readonly IWindowManager _windowManager;
        private readonly ScreenGrabberViewModel _grabberViewModel;
        private readonly IDispatcher _dispatcher;
        private readonly IEventAggregator _eventAggregator;
        private static KeyboardHookListener _listener;
        private static GlobalHooker _hooker;

        public KeyboardHookStartupTask(IStateHolder stateHolder, IWindowManager windowManager, ScreenGrabberViewModel grabberViewModel, IDispatcher dispatcher, IEventAggregator eventAggregator)
        {
            _stateHolder = stateHolder;
            _windowManager = windowManager;
            _grabberViewModel = grabberViewModel;
            _dispatcher = dispatcher;
            _eventAggregator = eventAggregator;
        }

        public int Priority
        {
            get { return 41; }
        }

        public void Execute()
        {
            _listener = new KeyboardHookListener(_hooker = new GlobalHooker());

            _listener.Start();

            _listener.KeyDown += ListenerKeyDown;
        }

        private void ListenerKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.PrintScreen && _stateHolder.State == ScreenGrabberState.Sleep)
            {
                _eventAggregator.Publish(ScreenGrabberState.Select);
                _windowManager.ShowWindow(_grabberViewModel);
            }
        }
    }
}