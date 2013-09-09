using EkranPaylas.Data.Domain;
using MongoDB.Driver;
using MongoRepository;

namespace EkranPaylas.Data.Repository
{
    public class ScreenShotRepository : MongoRepository<ScreenShot>, IRepository<ScreenShot>
    {
        public ScreenShotRepository() : base(new MongoUrl("mongodb://localhost:27017/test"))
        {

        }
    }
}
