using EkranPaylas.Utilities;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class UpdaterStartupTask : IStartupTask
    {
        private readonly IApplicationUpdater _applicationUpdater;
        public int Priority { get; private set; }

        public UpdaterStartupTask(IApplicationUpdater applicationUpdater)
        {
            _applicationUpdater = applicationUpdater;
        }

        public void Execute()
        {
            _applicationUpdater.Update();
        }
    }
}