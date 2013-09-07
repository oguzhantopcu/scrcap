namespace EkranPaylas.Uploaders.Infra
{
    public abstract class Uploader : IUploader
    {
        public abstract HostType Host { get;  }

        public virtual IProgress<UploadResult> StartUpload(byte[] data, string fileName)
        {
            var prog = new UploadProgress(() => new UploadResult {Result = Upload(data, fileName)});
            prog.ExecuteAsync();

            return prog;
        }

        public abstract string Upload(byte[] data, string fileName);
    }
}