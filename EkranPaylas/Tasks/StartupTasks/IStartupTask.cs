namespace EkranPaylas.Tasks.StartupTasks
{
    public interface IStartupTask
    {
        int Priority { get; }

        void Execute();
    }
}
