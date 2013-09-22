using System;
using System.IO;
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
        }

        public DateTime StartTime { get; protected set; }
        public double PercentOfComplete { get; protected set; }
        public event Action<UploadResult> Completed;

        public void ExecuteAsync()
        {
            _thread = new Thread(() => Proc());
            _thread.Start();
        }

        protected void Proc(int retryCount = 3)
        {
            try
            {
                var result = _proc();

                Completed(result);
            }
            catch (ThreadAbortException ex)
            {
                
            }
            catch (Exception x)
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\" + "error.txt",
                    x.Message + ":" + (x.InnerException != null ? x.InnerException.Message : ""));
                if (retryCount-- > 0)
                    Proc(retryCount);
                else Completed(null);
            }
        }

        public void Stop()
        {
            _thread.Abort();
        }
    }
}