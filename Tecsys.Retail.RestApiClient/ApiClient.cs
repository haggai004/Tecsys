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

        static bool _isInitialized = false;

        public ApiClient(HttpClient httpClient)
        {
            Client = httpClient;

            if (httpClient.BaseAddress==null)
            {
                httpClient.BaseAddress = new Uri(Settings.Default.BaseUri);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            
        }

        public HttpClient Client { get; }

        //private readonly IHttpClientFactory _httpClientFactory;

        //public ApiClient(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        //public HttpClient Client
        //{
        //    get
        //    {
        //        var httpClient = _httpClientFactory.CreateClient();
        //        httpClient.BaseAddress = new Uri(Settings.Default.BaseUri);
        //        httpClient.DefaultRequestHeaders.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        return httpClient;
        //    }
        //}
    }
}
