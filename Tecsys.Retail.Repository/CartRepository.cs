
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using Tecsys.Retail.Ef;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Tecsys.Retail.Repository
{
    public class CartRepository : Repository, ICartRepository
    {
        object _lockerObj = new object();
        public async Task<int> AddOrUpdateCartItemAsync(CartItem cartItem)
        {
            return await Task.Run(() =>
            {
                try
                {
                    //check if exists

                    CartItem cartItemEntity = null;

                    lock(_lockerObj)
                    {
                        cartItemEntity = DbContext.CartItems.FirstOrDefault(c => c.ItemId == cartItem.ItemId);
                    }

                    string sql;
                    object[] sqlParams = null;


                    if (cartItemEntity == null)
                    {
                        //This an insert of a new cart items
                        //Since data is coming from other layers in the application - need to handle SQL Injection security risk by 
                        //using prepared sql
                        var itemIdParam1 = new SqlParameter("@ItemId", cartItem.ItemId);
                        var cartIdParam1 = new SqlParameter("@CartId", cartItem.CartId);
                        var quantityParam1 = new SqlParameter("@Quantity", cartItem.Quantity);
                        var dateCreatedParam1 = new SqlParameter("@DateCreated", DateTime.Now);
                        var productIdParam1 = new SqlParameter("@ProductId", cartItem.ProductId);
                        sqlParams = new[] { itemIdParam1, cartIdParam1, quantityParam1, dateCreatedParam1, productIdParam1 };
                        sql = $@"INSERT INTO [dbo].[CartItems]([ItemId]
                           ,[CartId]
                           ,[Quantity]
                           ,[DateCreated]
                           ,[ProductId])
                     VALUES
                           (@ItemId
                           ,@CartId
                           ,@Quantity
                           ,@DateCreated
                           ,@ProductId)";

                        cartItemEntity = new CartItem();
                    }
                    else
                    {
                        //This is an update of cart items quantity
                        var itemIdParam2 = new SqlParameter("@ItemId", cartItem.ItemId);
                        var quantityParam2 = new SqlParameter("@Quantity", cartItem.Quantity);
                        sqlParams = new object[] { itemIdParam2, quantityParam2 };

                        sql = @"UPDATE [dbo].[CartItems] SET [Quantity] = (Quantity + @Quantity) WHERE [ItemId] = @ItemId";

                        cartItemEntity.Quantity += cartItem.Quantity;
                        //DbContext.CartItems.AddOrUpdate(cartItemEntity);
                    }

                    //DbContext is not thread safe - Need to handle multi-thread access contention by locking DbContext
                    lock (_lockerObj)
                    {
                        return DbContext.Database.ExecuteSqlCommand(sql, sqlParams);
                    }
                }
                catch (Exception ex)
                {
                    //log
                    return 0;

                }

            });
        }

        public async Task<CartItem> GetCartItemAsync(string itemId)
        {
            return await Task.Run(() =>
            {
                //DbContext is not thread safe - Need to handle multi-thread access contention contention by locking DbContext 
                try
                {
                    lock (_lockerObj)
                    {
                        return DbContext.CartItems.Find(new object[] { itemId });
                    }
                }
                catch (Exception ex)
                {
                    //log
                }

                return null;
            });
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string cartd)
        {
            return await Task.Run(() =>
            {
                //DbContext is not thread safe - Need to handle multi-thread access contention by locking DbContext
                try
                {
                    lock (_lockerObj)
                    {
                        return DbContext.CartItems.Where(c => c.CartId == cartd).ToList<CartItem>();
                    }
                }
                catch (Exception ex)
                {
                    //log
                }

                return null;
            });
        }

        public async Task<bool> UpdateCartItemAsync(CartItem cartItem)
        {
            await Task.Run(() =>
            {
                //DbContext is not thread safe - Need to handle multi-thread access contention by locking DbContext
                try
                {
                    lock (_lockerObj)
                    {
                        DbContext.CartItems.AddOrUpdate(cartItem);
                        DbContext.SaveChangesAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    //log;
                }

                return false;
            });

            return false;
        }
    }
}
