using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;
using Tecsys.Retail.Caching;

namespace Tecsys.Retail.RestApiClient
{
    public class ProductApiClient:ApiClient, IProductApiClient
    {
        const string Route = @"api/product/";
        public async Task<IEnumerable<IProductModel>> GetCars()
        {
            const string cacheKey = "GetCars";
            TsCache cache = new TsCache();
            var fromcache = cache.Get(cacheKey);
            if (fromcache != null)
                return fromcache;

            var json = await Client.GetStringAsync($"{Route}");
            var carModels = JsonConvert.DeserializeObject<List<ProductModel>>(json);

            if (carModels != null && carModels.Count > 0)
                cache.AddOrUpdate(cacheKey, carModels);

            return carModels;
        }

        public async Task<IEnumerable<IProductModel>> GetProducts(string searchText)
        {
            string cacheKey = searchText;
            TsCache cache = new TsCache();
            var fromcache = cache.Get(cacheKey);
            if (fromcache != null)
                return fromcache;

            var json = await Client.GetStringAsync($"{Route}/{searchText}");
            var productModels = JsonConvert.DeserializeObject<List<ProductModel>>(json);

            if (productModels != null && productModels.Count > 0)
                cache.AddOrUpdate(cacheKey, productModels);

            return productModels;
        }
    }
}
