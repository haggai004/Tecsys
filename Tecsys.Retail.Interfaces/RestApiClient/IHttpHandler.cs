using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tecsys.Retail.Interface.RestApiClient
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
        //HttpResponseMessage Get(string url);
        //HttpResponseMessage Post(string url, HttpContent content);
        //Task<HttpResponseMessage> GetAsync(string url);
        //Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}
