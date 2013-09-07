﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using EkranPaylas.UnitTests;
using EkranPaylas.Uploaders;
using EkranPaylas.Uploaders.Infra;
using Ploeh.AutoFixture;
using Xunit;

namespace EkranPaylas.IntegrationTests.Web
{
    public abstract class UploaderFacts : FixtureTestBase
    {
        public abstract IUploader GenerateUploader();

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