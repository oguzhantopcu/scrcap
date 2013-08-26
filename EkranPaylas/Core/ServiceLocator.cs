using Caliburn.Core.InversionOfControl;

namespace EkranPaylas.Core
{
    public static class ServiceLocator
    {
        public static IServiceLocator Current
        {
            get
            {
                var epApplication = System.Windows.Application.Current.Resources["Bootstrapper"] as Application;
                return epApplication != null ? epApplication.Container : null;
            }
        }
    }
}