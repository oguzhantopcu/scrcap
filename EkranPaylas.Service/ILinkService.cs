using System.ServiceModel;

namespace EkranPaylas.Service
{
    [ServiceContract]
    public interface IScreenShotService
    {
        [OperationContract]
        string CreateScreenShot(string[] shotLinks);

        [OperationContract]
        string[] GetScreenShotSources(string shotId);
    }
}
