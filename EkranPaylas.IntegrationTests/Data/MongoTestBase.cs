using MongoDB.Driver;
using MongoRepository;

namespace EkranPaylas.IntegrationTests.Data
{
    public abstract class MongoTestBase<TDocument>
        where TDocument : IEntity
    {
        protected readonly MongoRepository<TDocument> Repository;

        protected MongoTestBase()
        {
            Repository = new MongoRepository<TDocument>(new MongoUrl("mongodb://localhost:27017/test"));
        }
    }
}