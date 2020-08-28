

using System.Threading.Tasks;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Ef;

namespace Tecsys.Retail.Repository
{
    public interface ICartRepository
    {
        Task<bool> AddCartItem(CartItem cartItem);
        Task<CartItem> GetCartItem(string itemId);
    }
}
