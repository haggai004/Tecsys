using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Tecsys.Retail.RestApiClient.Properties;

namespace Tecsys.Retail.RestApiClient
{
    public class ApiClient
    {
        //https://localhost:44326/api/product/
        public ApiClient()
        {
           BaseUri = Settings.Default.BaseUri;
           this.Client = new HttpClient { BaseAddress = new Uri(BaseUri) };
           this.Client.DefaultRequestHeaders.Clear();
            //Define request data format  
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public string BaseUri { get; }
        public HttpClient Client { get; }
    }
}
