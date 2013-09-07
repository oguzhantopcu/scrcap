using System.Linq;
using EkranPaylas.Core;

namespace EkranPaylas.Uploaders.Infra
{
    public class ImageUploaderFactory : IUploaderFactory
    {
        public IUploader GetUploader(HostType hostType)
        {
            return ServiceLocator.Current.GetAllInstances(typeof (IUploader))
                .Cast<IUploader>()
                .FirstOrDefault(q => q.Host == hostType);
        }
    }
}