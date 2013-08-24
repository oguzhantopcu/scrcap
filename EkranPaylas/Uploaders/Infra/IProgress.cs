using System;

namespace EkranPaylas.Uploaders.Infra
{
    public interface IProgress<TResult>
    {
        DateTime StartTime { get; }

        event Action<TResult> Completed;

        void ExecuteAsync();
        void Stop();
    }
}