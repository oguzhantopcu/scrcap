namespace EkranPaylas.Uploaders.Infra
{
    public interface IUploader
    {
        HostType Host { get; }

        IProgress<UploadResult> StartUpload(byte[] data, string fileName);

        string Upload(byte[] data, string fileName);
    }
}