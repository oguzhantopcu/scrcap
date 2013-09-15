//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using EkranPaylas.Uploaders.Infra;
//using HtmlAgilityPack;
//using RestSharp;

//namespace EkranPaylas.Uploaders
//{
//    public class PostimgUploader : Uploader
//    {
//        private readonly IRestClient _restClient;

//        public PostimgUploader(IRestClient restClient)
//        {
//            _restClient = restClient;
//            _restClient.CookieContainer = new CookieContainer();
//            _restClient.UserAgent =
//                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.66 Safari/537.36";
//        }

//        public override HostType Host
//        {
//            get { return HostType.Postimg; }
//        }

//        public override string Upload(byte[] data, string fileName)
//        {
//            var parameters = GetParameters();
            
//            var request = new RestRequest { Method = Method.POST, Resource = "http://postimage.org/index.php" };
//            request.AddFile("upload", data, fileName);
//            parameters.ForEach(q => request.AddParameter(q.Name, q.Value));
//            //parameters.ForEach(q => request.AddParameter(q));
//            request.AddParameter("optsize", "0");
//            request.AddParameter("optsize", "0",ParameterType.Cookie);

//            request.AddParameter("adult", "no", ParameterType.Cookie);
//            request.AddParameter("um", "computer", ParameterType.Cookie);
//            var response = _restClient.Execute(request);

//            return ExtractPureUriFromResult(response.Content);
//        }

//        public string ExtractPureUriFromResult(string content)
//        {
//            return content.Split(new[] {"id=\"code_2\" scrolling=\"no\">"}, StringSplitOptions.None)[1].Split('<').First();
//        }

//        public List<Parameter> GetParameters()
//        {
//            var document = new HtmlDocument();
//            document.LoadHtml(_restClient.Get(new RestRequest("http://postimage.org")).Content);

//            var pars = document.GetElementbyId("myForm").SelectNodes(".//input")
//                .Where(o => o.Attributes.Contains("name") && o.Attributes["name"] != null)
//                .Select(q => new Parameter {Name = q.Attributes["name"].Value, Value = q.Attributes.Contains("value") ? q.Attributes["value"].Value : ""})
//                .ToList();

//            var sessionId = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds);
//            pars.Remove(pars.First(o => Equals(o.Value, "yes") && Equals(o.Name, "adult")));
//            pars.First(q => q.Name == "session_upload").Value = sessionId;
//            return pars;
//        }
//    }
//}
