using System;
using System.Globalization;
using System.Linq;
using EkranPaylas.Data.Domain;
using EkranPaylas.Data.Repository;
using EkranPaylas.Service;
using Moq;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace EkranPaylas.UnitTests
{
    public class ScreenShotServiceFacts : FixtureTestBase
    {
        private readonly Mock<IRepository<ScreenShot>> _mockRepository;
        private readonly ScreenShotService _sut;
        private readonly Mock<IStringGenerator> _mockGenerator;

        public ScreenShotServiceFacts()
        {
            _mockRepository = new Mock<IRepository<ScreenShot>>();
            _mockGenerator = new Mock<IStringGenerator>();

            _mockGenerator.Setup(f => f.GenerateString(It.IsAny<StringGenerateOptions>(), It.IsAny<int>()))
                .Returns(() => Guid.NewGuid().ToString());

            _sut = new ScreenShotService(_mockRepository.Object, _mockGenerator.Object);
        }

        [Theory, AutoData]
        public void CreateScreenShot_Should_Create_ScreenShot(Uri[] uris)
        {
            var sUris = uris.Select(f => f.AbsoluteUri.ToString(CultureInfo.InvariantCulture)).ToArray();
            _sut.CreateScreenShot(sUris);

            _mockRepository.Verify(f => f.Add(It.Is<ScreenShot>(t => t.Links == sUris)));
        }

        [Theory, AutoData]
        public void CreateScreenShot_Should_Get_Id_From_Generator(Uri[] uris)
        {
            var sUris = uris.Select(f => f.AbsoluteUri.ToString(CultureInfo.InvariantCulture)).ToArray();
            _sut.CreateScreenShot(sUris);

            _mockGenerator.Verify(
                f => f.GenerateString(It.Is<StringGenerateOptions>(q => q == StringGenerateOptions.IncludeCharAndDigits),
                    It.Is<int>(t => t == 5)));
        }

        [Theory, AutoData]
        public void GetScreenShotSources_Should_Get_Uris(ScreenShot screenShot)
        {
            _mockRepository.Setup(f => f.GetById(screenShot.Id)).Returns(screenShot);

            var sources = _sut.GetScreenShotSources(screenShot.Id);

            Assert.Equal(sources, screenShot.Links);
        }

        [Theory, AutoData]
        public void GetScreenShotSources_Should_Give_Null_When_Id_Is_Not_Exist(string randomId)
        {
            _mockRepository.Setup(f => f.GetById(randomId)).Returns((ScreenShot)null);

            var sources = _sut.GetScreenShotSources(randomId);

            Assert.Null(sources);
        }
    }
}