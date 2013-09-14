using System;
using System.Deployment.Application;
using Microsoft.Win32;

namespace EkranPaylas.Utilities
{
    public class ClickOnceStarter : IApplicationStarter
    {
        public string ApplicationName { get; set; }
        public string PublisherName { get; set; }

        public string ApplicationLink
        {
            get
            {
                return string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "\\", PublisherName,
                    "\\", ApplicationName, ".appref-ms");
            }
        }

        public bool IsNetworkDeployed
        {
            get { return ApplicationDeployment.IsNetworkDeployed; }
        }

        public bool StartsWithSystem
        {
            get
            {
                var registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                return registryKey != null && registryKey.GetValue(ApplicationName) != null;
            }
            set
            {
                if (value) SetAsStartup();
                else RemoveFromStartup();
            }
        }

        private void SetAsStartup()
        {
            if (!ApplicationDeployment.IsNetworkDeployed) return;
            var registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (registryKey != null)
                registryKey.SetValue(ApplicationName, ApplicationLink);
        }

        private void RemoveFromStartup()
        {
            if (!ApplicationDeployment.IsNetworkDeployed) return;
            var registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            if (registryKey != null)
                registryKey.DeleteValue(ApplicationName);
        }
    }
}