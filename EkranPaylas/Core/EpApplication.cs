﻿using System.Linq;
using System.Windows;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EkranPaylas.Extensions;
using EkranPaylas.Graphic;
using EkranPaylas.Tasks.StartupTasks;
using EkranPaylas.Uploaders.Infra;
using EkranPaylas.ViewModels;

namespace EkranPaylas.Core
{
    public class Application : Bootstrapper<ScreenGrabberViewModel>
    {
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            typeof(IStartupTask).GetInstances<IStartupTask>()
                                 .OrderBy(q => q.Priority)
                                 .ToList()
                                 .ForEach(q => q.Execute());

            base.OnStartup(sender, e);
        }

        protected override IServiceLocator CreateContainer()
        {
            var container = new WindsorContainer();
            var adapter = new WindsorAdapter(container);

            container.Register(Component.For<IScreenGrabber, ScreenGrabber>().LifestyleSingleton());
            container.Register(Component.For<IUploaderFactory, ImageUploaderFactory>().LifestyleSingleton());
            container.Register(Classes.FromThisAssembly()
                .Where(x => x.Name.EndsWith("ViewModel"))
                .WithServiceDefaultInterfaces()
                .LifestyleSingleton());

            return adapter;
        }
    }
}