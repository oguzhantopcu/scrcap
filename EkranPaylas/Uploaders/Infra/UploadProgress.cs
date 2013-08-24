using System;
using System.Threading;

namespace EkranPaylas.Uploaders.Infra
{
    public class UploadProgress : IProgress<UploadResult>
    {
        private readonly Func<UploadResult> _proc;
        private Thread _thread;

        public UploadProgress(Func<UploadResult> proc)
        {
            _proc = proc;
            _proc();
        }

        public DateTime StartTime { get; protected set; }
        public double PercentOfComplete { get; protected set; }
        public event Action<UploadResult> Completed;

        public void ExecuteAsync()
        {
            _thread = new Thread(Proc);
            _thread.Start();
        }

        protected void Proc()
        {
            var result = _proc();

            Completed(result);
        }

        public void Stop()
        {
            _thread.Abort();
        }
    }
}