using System;
using System.Collections.Generic;
using System.Linq;
using EkranPaylas.Extensions;
using EkranPaylas.Uploaders.Infra;
using HtmlAgilityPack;
using RestSharp;

namespace EkranPaylas.Uploaders
{
    public class PostimgUploader : IUploader
    {
        public HostType Host
        {
            get { return HostType.Postimg; }
        }

        public IProgress<UploadResult> StartUpload(byte[] data, string fileName)
        {
            var prog =  new UploadProgress(() => new UploadResult {Result = Upload(data, fileName)});
            prog.ExecuteAsync();

            return prog;
        }

        public string Upload(byte[] data, string fileName)
        {
            var parameters = GetParameters();

            var client = new RestClient();
            
            var request = new RestRequest { Method = Method.POST, Resource = "http://postimage.org/index.php" };
            request.AddFile("upload", data, fileName);
            request.Parameters.AddRange(parameters);
            
            var response = client.Execute(request);
            var nextUri = string.Join("/", response.ResponseUri.AbsoluteUri.Split('/').Take(5).ToArray());
            var content = client.Execute(new RestRequest(nextUri, Method.GET)).Content;

            var uri = ExtractPureUriFromResult(content);

            client.Execute(new RestRequest(uri, Method.GET));

            return uri;
        }

        public string ExtractPureUriFromResult(string content)
        {
            return "http://s" +
                   content.Split(new[] {"http://s"}, StringSplitOptions.None)[1]
                       .Split(new[] {"'"}, StringSplitOptions.None).First();
        }

        public List<Parameter> GetParameters()
        {
            var htmlWeb = new HtmlWeb();
            var document = htmlWeb.Load("http://postimage.org");

            var pars = document.GetElementbyId("myForm").Elements("input")
                        .Select(q => new Parameter { Name = q.Attributes["name"].Value, Value = q.Attributes["value"].Value })
                        .ToList();

            var sessionId = DateTime.UtcNow.GetTime();

            pars.First(q => q.Name == "session_upload").Value = sessionId;

            return pars;
        }
    }
}
