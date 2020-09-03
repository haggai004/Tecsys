using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;
using Tecsys.Retail.Caching;
using System.Net.Http;

namespace Tecsys.Retail.RestApiClient
{
    public class ProductApiClient:ApiClient, IProductApiClient
    {
        const string Route = @"api/product/";
        public ProductApiClient(HttpClient httpClient):base(httpClient)
        {
        }

        //public ProductApiClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        //{
        //}

        public async Task<IEnumerable<IProductModel>> GetCarsAsync()
        {
            const string cacheKey = "GetCars";
            TsCache cache = new TsCache();
            var fromcache = cache.Get(cacheKey);
            if (fromcache != null)
                return fromcache;

            string uri = $"{Route}";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await Client.SendAsync(requestMessage);
            var json = response.Content.ReadAsStringAsync().Result;
            var carModels = JsonConvert.DeserializeObject<List<ProductModel>>(json);

            if (carModels != null && carModels.Count > 0)
                cache.AddOrUpdate(cacheKey, carModels);

            return carModels;
        }

        public async Task<IEnumerable<IProductModel>> GetProductsAsync(string searchText)
        {
            string cacheKey = searchText;
            TsCache cache = new TsCache();
            var fromcache = cache.Get(cacheKey);
            if (fromcache != null)
                return fromcache;

            string uri = $"{Route}/{searchText}";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await Client.SendAsync(requestMessage);
            var json = response.Content.ReadAsStringAsync().Result;
            var productModels = JsonConvert.DeserializeObject<List<ProductModel>>(json);

            if (productModels != null && productModels.Count > 0)
                cache.AddOrUpdate(cacheKey, productModels);

            return productModels;
        }
    }
}
