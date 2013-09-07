using EkranPaylas.Uploaders;
using EkranPaylas.Uploaders.Infra;
using RestSharp;

namespace EkranPaylas.IntegrationTests.Web
{
    public class PostimgUploaderFacts : UploaderFacts
    {
        public override IUploader GenerateUploader()
        {
            return new PostimgUploader(new RestClient());
        }
    }
}