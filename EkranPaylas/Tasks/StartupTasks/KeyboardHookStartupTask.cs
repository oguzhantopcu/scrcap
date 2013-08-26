using System.Windows.Forms;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using EkranPaylas.Core;
using EkranPaylas.ViewModels;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class KeyboardHookStartupTask : IStartupTask
    {
        private static KeyboardHookListener _listener;
        private static GlobalHooker _hooker;

        public int Priority
        {
            get { return 41; }
        }

        public void Execute()
        {
            _listener = new KeyboardHookListener(_hooker = new GlobalHooker());

            _listener.Start();

            _listener.KeyDown += _listener_KeyDown;
        }

        private void _listener_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.PrintScreen)
                ServiceLocator.Current.GetInstance<IEventAggregator>().Publish(ScreenGrabberState.Select);
        }
    }
}