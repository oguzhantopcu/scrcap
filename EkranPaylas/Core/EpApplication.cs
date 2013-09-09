using System;
using System.Linq;
using System.Windows;
using Caliburn.Core.Configuration;
using Caliburn.Core.InversionOfControl;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using EkranPaylas.Graphic;
using EkranPaylas.Tasks.StartupTasks;
using EkranPaylas.Uploaders.Infra;
using EkranPaylas.Utilities;
using EkranPaylas.ViewModels;
using RestSharp;

namespace EkranPaylas.Core
{
    public class Application : Bootstrapper<MainViewModel>
    {
        private WindsorContainer _container;

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            _container.ResolveAll<IStartupTask>()
                .OrderBy(q => q.Priority)
                .ToList()
                .ForEach(q => q.Execute());
            
            base.OnStartup(sender, e);
        }

        protected override IServiceLocator CreateContainer()
        {
            _container = new WindsorContainer();
            var adapter = new WindsorAdapter(_container);

            _container.Kernel.Resolver.AddSubResolver(new ArrayResolver(_container.Kernel, true));
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));

            _container.Register(Component.For<IRestClient, RestClient>().LifestyleSingleton());
            _container.Register(Component.For<IUploaderFactory, ImageUploaderFactory>().LifestyleSingleton());
            _container.Register(Component.For<IScreenGrabber, ScreenGrabber>().LifestyleSingleton());
            _container.Register(Component.For<IStateHolder, StateHolder>().LifestyleSingleton());

            _container.Register(Classes.FromThisAssembly()
                .Where(x => x.Name.EndsWith("Uploader"))
                .WithServiceDefaultInterfaces()
                .LifestyleSingleton());

            _container.Register(Classes.FromThisAssembly()
                .Where(x => x.Name.EndsWith("Task"))
                .WithServiceDefaultInterfaces()
                .LifestyleSingleton());

            _container.Register(Classes.FromThisAssembly()
                .Where(x => x.Name.EndsWith("ViewModel"))
                .WithServiceDefaultInterfaces()
                .LifestyleSingleton());

            return adapter;
        }
    }
}