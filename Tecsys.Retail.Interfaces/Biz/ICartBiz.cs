using System.Collections.Generic;
using System.Threading.Tasks;
using Tecsys.Retail.Domain;

namespace Tecsys.Retail.Biz
{
    public interface ICartService
    {
        Task<ICartItem> CreateCartItemAsync(string cartId, int productId);
        Task<bool> AddCartItemAsync(ICartItem cartItem);
        Task<bool> UpdateCartItemAsync(ICartItem cartItem);
        Task<ICartItem> GetCartItemAsync(string itemId);
        Task<List<ICartItem>> GetCartItemsAsync(string cartId);
        Task<ICart> GetCartAsync(string cartId);
    }
}
 