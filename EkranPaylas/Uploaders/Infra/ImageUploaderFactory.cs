using System.Collections.Generic;
using System.Linq;
using EkranPaylas.Extensions;

namespace EkranPaylas.Uploaders.Infra
{
    public class ImageUploaderFactory : IUploaderFactory
    {
        private readonly List<IUploader> _uploaders;

        public ImageUploaderFactory()
        {
            _uploaders = typeof (IUploader).GetInstances<IUploader>().ToList();
        }

        public IUploader GetUploader(HostType hostType)
        {
            return _uploaders.FirstOrDefault(q => q.Host == hostType);
        }
    }
}