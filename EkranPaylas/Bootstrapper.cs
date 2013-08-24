using System.Linq;
using EkranPaylas.Extensions;
using EkranPaylas.Tasks.StartupTasks;

namespace EkranPaylas
{
    public static class Bootstrapper
    {
        public static void Start()
        {
            typeof (IStartupTask).GetInstances<IStartupTask>()
                                 .OrderBy(q => q.Priority)
                                 .ToList()
                                 .ForEach(q => q.Execute());
        }
    }
}
