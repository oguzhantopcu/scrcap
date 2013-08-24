using EkranPaylas.Data.Domain;
using MongoDB.Driver.Wrappers;
using MongoRepository;

namespace EkranPaylas.Data.Repository
{
    public class ScreenShotRepository : MongoRepository<ScreenShot>, IRepository<ScreenShot>
    {
    }
}
