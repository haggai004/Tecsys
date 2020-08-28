

using System.Threading.Tasks;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Ef;
using System.Collections.Generic;

namespace Tecsys.Retail.Repository
{
    public interface ICartRepository
    {
        Task<bool> AddCartItemAsync(CartItem cartItem);
        Task<bool> UpdateCartItemAsync(CartItem cartItem);
        Task<CartItem> GetCartItemAsync(string itemId);
        Task<List<CartItem>> GetCartItemsAsync(string cartd);

        ///// <summary>
        ///// Not supported by DB Schema
        ///// </summary>
        ///// <param name="cartId"></param>
        ///// <returns></returns>
        //Task<ICart> GetCart(string cartId);

    }
}
