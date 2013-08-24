using System.Reflection;

namespace EkranPaylas.Uploaders.Infra
{
    public interface IUploaderFactory
    {
        IUploader GetUploader(HostType hostType);
    }
}