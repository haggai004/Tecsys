using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Tecsys.Retail.Interface.RestApiClient;
using System.Web;

namespace Tecsys.Retail.RestApiClient
{
    public class CustomHttpHandler : DelegatingHandler,IHttpHandler
    {

        public CustomHttpHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            CancellationToken token = new CancellationToken();
            return await base.SendAsync(request,token);
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request,cancellationToken);
        }
    }

}
