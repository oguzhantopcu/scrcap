using System.ServiceModel;

namespace EkranPaylas.Service
{
    [ServiceContract]
    public interface IScreenShotService
    {
        [OperationContract]
        string New(string[] shotLinks);

        [OperationContract]
        string[] GetSources(string shotId);
    }
}
