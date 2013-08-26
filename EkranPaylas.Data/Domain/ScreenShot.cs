using MongoRepository;

namespace EkranPaylas.Data.Domain
{
    public class ScreenShot : IEntity
    {
        public string Id { get; set; }

        public string[] Links { get; set; }

        public byte[] UploaderIpAddress { get; set; }
    }
}
