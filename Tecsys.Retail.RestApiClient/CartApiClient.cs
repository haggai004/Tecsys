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

        public CartApiClient(TypeMapping.ITypeMapper typeMapper)
        {
            _typeMapper = typeMapper;
        }

        public async Task<HttpResponseMessage> AddCartItemAsync(Model.ICartItemModel cartItemModel)
        {
            string content = JsonConvert.SerializeObject(cartItemModel);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync($"{Route}/", httpContent);
            return response;
        }

        public async Task<ICartItemModel> CreateCartItemAsync(string cartId, int productId)
        {
            var json = await Client.GetStringAsync($"api/Cart/NewCartItemGet?cartId={cartId}&productId={productId}");
            CartItem cartItem = JsonConvert.DeserializeObject<Domain.CartItem>(json);
            CartItemModel cartItemModel = _typeMapper.Map<CartItem, CartItemModel>(cartItem);
            return cartItemModel;
        }

        public async Task<ICartItemModel> GetCartItemAsync(string itemId)
        {
            var json = await Client.GetStringAsync($"api/Cart/itemId={itemId}");
            return JsonConvert.DeserializeObject<ICartItemModel>(json);
        }
    }
}
