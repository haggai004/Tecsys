using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tecsys.Retail.Domain;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;

namespace Tecsys.Retail.RestApiClient
{
    public class CartApiClient: ApiClient, ICartApiClient
    {
        TypeMapping.ITypeMapper _typeMapper;
        const string Route = @"api/Cart/";

        public CartApiClient(HttpClient httpClient, TypeMapping.ITypeMapper typeMapper):base(httpClient)
        {
            _typeMapper = typeMapper;
        }

        //public CartApiClient(IHttpClientFactory httpClientFactory, TypeMapping.ITypeMapper typeMapper) : base(httpClientFactory)
        //{
        //    _typeMapper = typeMapper;
        //}

        public async Task<HttpResponseMessage> AddCartItemAsync(Model.ICartItemModel cartItemModel)
        {
            string content = JsonConvert.SerializeObject(cartItemModel);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            string uri = $"{Route}/";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.Content = httpContent;
            HttpResponseMessage response = await Client.SendAsync(requestMessage);
            return response;
        }

        public async Task<ICartItemModel> CreateCartItemAsync(string cartId, int productId)
        {
            string uri = $"api/Cart/NewCartItemGet?cartId={cartId}&productId={productId}";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await Client.SendAsync(requestMessage);
            var json = response.Content.ReadAsStringAsync().Result;
            CartItem cartItem = JsonConvert.DeserializeObject<Domain.CartItem>(json);
            CartItemModel cartItemModel = _typeMapper.Map<CartItem, CartItemModel>(cartItem);
            return cartItemModel;
        }

        public async Task<ICartItemModel> GetCartItemAsync(string itemId)
        {
            string uri = $"api/Cart/GetCartItemAsync?itemId={itemId}";
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            HttpResponseMessage response = await Client.SendAsync(requestMessage);
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<CartItemModel>(json);
        }
    }
}
