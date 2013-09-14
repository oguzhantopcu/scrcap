using System;
using System.Linq;
using EkranPaylas.Uploaders.Infra;
using RestSharp;

namespace EkranPaylas.Uploaders
{
    public class EkranPaylasUploader : Uploader
    {
        private readonly IUploaderFactory _uploaderFactory;
        private readonly IRestClient _restClient;

        public EkranPaylasUploader(IUploaderFactory uploaderFactory, IRestClient restClient)
        {
            _uploaderFactory = uploaderFactory;
            _restClient = restClient;
        }

        public override HostType Host
        {
            get { return HostType.EkranPaylasHost; }
        }

        protected string ServiceAddress
        {
            get { return "http://localhost:6789"; }
        }

        protected string HostAddress
        {
            get { return "http://localhost:6789"; }
        }

        protected string NewPath
        {
            get { return "/New"; }
        }

        public override string Upload(byte[] data, string fileName)
        {
            var links = Enum.GetValues(typeof (HostType))
                .Cast<HostType>()
                .Where(q => !q.Equals(Host))
                .Select(_uploaderFactory.GetUploader)
                .Select(q => q.Upload(data, fileName))
                .ToArray();

            return links.First();

            var request = new RestRequest {Method = Method.POST, Resource = ServiceAddress + NewPath};
            request.AddParameter("links", links);
            var response = _restClient.Execute(request);

            return HostAddress + "/" + response.Content;
        }
    }
}