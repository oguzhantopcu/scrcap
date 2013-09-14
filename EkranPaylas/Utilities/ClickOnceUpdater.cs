using System;
using System.ComponentModel;
using System.Deployment.Application;
using System.Threading;

namespace EkranPaylas.Utilities
{
    public class ClickOnceUpdater : IApplicationUpdater
    {
        public string ApplicationName { get; set; }
        public string PublisherName { get; set; }

        public string ApplicationLink
        {
            get
            {
                return string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "\\", PublisherName, "\\", ApplicationName, ".appref-ms");
            }
        }

        public bool IsNetworkDeployed
        {
            get { return ApplicationDeployment.IsNetworkDeployed; }
        }

        public void Update()
        {
            if (!IsNetworkDeployed) return;

            ThreadPool.QueueUserWorkItem(q =>
            {
                var updateRequired = ApplicationDeployment.CurrentDeployment.CheckForUpdate();

                if (!updateRequired) return;
                ApplicationDeployment.CurrentDeployment.UpdateCompleted += UpdateCheckUpdateCompleted;
                ApplicationDeployment.CurrentDeployment.UpdateAsync();
            });
        }

        private void UpdateCheckUpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Restart();
        }

        public void Restart()
        {
            System.Diagnostics.Process.Start(ApplicationLink);
            Environment.Exit(Environment.ExitCode);
        }
    }
}