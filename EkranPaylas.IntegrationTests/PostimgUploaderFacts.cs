using EkranPaylas.Uploaders;

namespace EkranPaylas.IntegrationTests
{
    public class PostimgUploaderFacts : UploaderFacts
    {
        public override IUploader GenerateUploader()
        {
            return new PostimgUploader();
        }
    }
}