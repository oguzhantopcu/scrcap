﻿using System;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EkranPaylas.Uploaders.Infra
{
    public abstract class Uploader : IUploader
    {
        public abstract HostType Host { get;  }

        public virtual IProgress<UploadResult> StartUpload(byte[] data, string fileName)
        {
            var prog = new UploadProgress(() => new UploadResult {Result = Upload(data, fileName)});
            prog.ExecuteAsync();

            return prog;
        }

        public abstract string Upload(byte[] data, string fileName);
    }

    public class ImgurUploader : Uploader
    {
        private readonly IRestClient _restClient;

        public override HostType Host
        {
            get { return HostType.Imgur;}
        }

        public ImgurUploader(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public override string Upload(byte[] data, string fileName)
        {
            var request = new RestRequest { Method = Method.POST, Resource = "https://api.imgur.com/3/image.json" };
            request.AddHeader("Authorization", "Client-ID 0b6a8a897f2b253");
            request.AddParameter("image", Convert.ToBase64String(data));

            var res = JObject.Parse(_restClient.Execute(request).Content);
            return res["data"]["link"].ToString();
        }
    }
}