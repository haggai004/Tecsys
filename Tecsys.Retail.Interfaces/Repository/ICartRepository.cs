

using System.Threading.Tasks;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Ef;
using System.Collections.Generic;

namespace Tecsys.Retail.Repository
{
    public interface ICartRepository:IRepository
    {
        Task<int> AddOrUpdateCartItemAsync(CartItem cartItem);
        Task<CartItem> GetCartItemAsync(string itemId);
        Task<List<CartItem>> GetCartItemsAsync(string cartId);

    }
}
