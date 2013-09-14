using EkranPaylas.Utilities;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class ApplicationUpdaterTask : IStartupTask
    {
        private readonly IApplicationUpdater _applicationUpdater;
        public int Priority { get; private set; }

        public ApplicationUpdaterTask(IApplicationUpdater applicationUpdater)
        {
            _applicationUpdater = applicationUpdater;
        }

        public void Execute()
        {
            _applicationUpdater.Update();
        }
    }
}