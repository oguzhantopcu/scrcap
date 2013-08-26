using EkranPaylas.Uploaders;

namespace EkranPaylas.IntegrationTests.Web
{
    public class PostimgUploaderFacts : UploaderFacts
    {
        public override IUploader GenerateUploader()
        {
            return new PostimgUploader();
        }
    }
}