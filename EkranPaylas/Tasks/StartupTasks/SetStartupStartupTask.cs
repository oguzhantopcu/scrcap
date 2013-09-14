using EkranPaylas.Utilities;

namespace EkranPaylas.Tasks.StartupTasks
{
    public class SetStartupStartupTask : IStartupTask
    {
        private readonly IApplicationStarter _starter;
        public int Priority { get; private set; }

        public SetStartupStartupTask(IApplicationStarter starter)
        {
            _starter = starter;
        }

        public void Execute()
        {
            _starter.StartsWithSystem = true;
        }
    }
}