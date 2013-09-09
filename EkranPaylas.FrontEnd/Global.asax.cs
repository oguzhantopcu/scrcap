using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Mvc;
using EkranPaylas.Data.Repository;
using EkranPaylas.FrontEnd.App_Start;
using EkranPaylas.FrontEnd.Controllers;
using EkranPaylas.Service;

namespace EkranPaylas.FrontEnd
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private WindsorContainer _windsorContainer;

        protected void Application_Start()
        {
            SetWindsor();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        public void SetWindsor()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Kernel.Resolver.AddSubResolver(new ArrayResolver(_windsorContainer.Kernel, true));
            _windsorContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(_windsorContainer.Kernel));

            _windsorContainer.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());
            _windsorContainer.Register(Classes.FromAssemblyContaining(typeof(IRepository<>))
                .BasedOn(typeof(IRepository<>))
                .WithServiceAllInterfaces()
                .LifestyleSingleton());
            _windsorContainer.Register(Classes.FromAssemblyContaining(typeof (IScreenShotService))
                .Pick()
                .WithServiceDefaultInterfaces()
                .LifestyleSingleton());
            _windsorContainer.Register(Classes.FromThisAssembly()
                .Pick()
                .WithServiceDefaultInterfaces()
                .LifestyleSingleton());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(_windsorContainer.Kernel));
        }
    }
}