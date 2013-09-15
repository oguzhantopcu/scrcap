using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using EkranPaylas.UnitTests;
using EkranPaylas.Uploaders.Infra;
using Ploeh.AutoFixture;
using RestSharp;
using Xunit;

namespace EkranPaylas.IntegrationTests.Web
{
    public abstract class UploaderFacts<TUploader> : FixtureTestBase where TUploader : IUploader
    {
        protected UploaderFacts()
        {
            this.Register<IRestClient>(() => new RestClient());
        }

        public virtual IUploader GenerateUploader()
        {
            return this.Create<TUploader>();
        }

        protected byte[] GenerateImage()
        {
            using (var memoryStream = new MemoryStream())
            {
                Fixture.Create<Bitmap>().Save(memoryStream, ImageFormat.Png);

                return memoryStream.GetBuffer();
            }
        }

        [Fact]
        public void Upload_Should_Upload_Valid_Image_And_Return_Image_Url()
        {
            var bytes = GenerateImage();

            var result = GenerateUploader().Upload(bytes, Random.Next() + ".png");
            byte[] data;
            using (var webClient = new WebClient()) data = webClient.DownloadData(result);

            Assert.Equal(data, bytes);
        }
    }
}