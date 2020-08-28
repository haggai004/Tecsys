using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Ef;

namespace Tecsys.Retail.Repository
{
    public class CartRepository:Repository, ICartRepository
    {
        object locker = new object();
        public async Task<bool> AddCartItem(CartItem cartItem)
        {
            await Task.Run(() =>
            {
                lock (locker)
                {
                    DbContext.CartItems.AddOrUpdate(cartItem);
                }
            });
                
            await DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CartItem> GetCartItem(string itemId)
        {
            return await Task.Run(() => DbContext.CartItems.Find(new object[]{itemId}));
        }
    }
}
