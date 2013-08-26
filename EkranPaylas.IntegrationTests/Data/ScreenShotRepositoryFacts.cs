using EkranPaylas.Data.Domain;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace EkranPaylas.IntegrationTests.Data
{
    public class ScreenShotRepositoryFacts : MongoTestBase<ScreenShot>
    {
        [Theory, AutoData]
        public void Add_Should_Add_Document_To_Db(ScreenShot screenShot)
        {
            Repository.Add(screenShot);

            Assert.NotNull(Repository.GetById(screenShot.Id));
        }

        [Theory, AutoData]
        public void Remove_Should_Remove_Document_From_Db(ScreenShot screenShot)
        {
            Repository.Add(screenShot);

            Repository.Delete(screenShot);

            Assert.Null(Repository.GetById(screenShot.Id));
        }

        [Theory, AutoData]
        public void Get_Should_Get_Document_From_Db(ScreenShot screenShot)
        {
            Repository.Add(screenShot);

            Assert.Equal(Repository.GetById(screenShot.Id).UploaderIpAddress, screenShot.UploaderIpAddress);
        }
    }
}