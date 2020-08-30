using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tecsys.Retail.Model;

namespace Tecsys.Retail.RestApiClient
{
    public interface ICartApiClient
    {
        Task<ICartItemModel> CreateCartItemAsync(string cartId,int productId);
        Task<HttpResponseMessage> AddCartItemAsync(Model.ICartItemModel cartItemModel);
        Task<ICartItemModel> GetCartItemAsync(string itemId);
    }
}
