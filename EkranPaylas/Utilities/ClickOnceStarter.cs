using System;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;

namespace EkranPaylas.Utilities
{
    public class ClickOnceStarter : IApplicationStarter
    {
        public ClickOnceStarter()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                PublisherName = GetPublisher();
                ApplicationName = "EkranPaylas";
            }
        }

        public static string GetPublisher()
        {
            XDocument xDocument;
            using (var memoryStream = new MemoryStream(AppDomain.CurrentDomain.ActivationContext.DeploymentManifestBytes))
            using (var xmlTextReader = new XmlTextReader(memoryStream))
                xDocument = XDocument.Load(xmlTextReader);
            
            var description = xDocument.Root.Elements().First(e => e.Name.LocalName == "description");
            var publisher = description.Attributes().First(a => a.Name.LocalName == "publisher");
            return publisher.Value;
        }

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