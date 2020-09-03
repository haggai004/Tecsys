using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecsys.Retail.Repository
{
    public class ProductRepository:Repository,IProductRepository
    {
        object _lockerObj = new object();

        public async Task<List<Ef.Product>> GetCars()
        {
            return await Task.Run(() =>
            {
                //DbContext is not thread safe - Need to handle multi-thread access contention by locking DbContext
                lock (_lockerObj)
                {
                    try
                    {
                        return DbContext.Products.Where(p => p.Category.CategoryName == "Cars").ToList();
                    }
                    catch (Exception ex)
                    {
                        //log
                    }

                    return null;
                }

            });

        }

        public async Task<List<Ef.Product>> GetProducts(string searchText)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(searchText))
                        throw new ArgumentNullException("searchText");
                    else if (searchText.Trim().Length < 2)
                        throw new ArgumentException($"Error: searchText length:{searchText.Length} is invalid. Minimum allowed is 2 chars ");

                    //DbContext is not thread safe - Need to handle multi-thread access contention by locking DbContext
                    lock (_lockerObj)
                    {
                        return DbContext.Products.Where(p =>
                            p.ProductName.ToLower().Contains(searchText.ToLower())
                            || p.Description.ToLower().Contains(searchText.ToLower())).ToList();
                    }
                }
                catch (Exception ex)
                {
                    //log
                }

                return null;
            });
        }

        public async Task<Ef.Product> GetProductAsync(int productId)
        {
            return await Task.Run(() =>
            {
                //DbContext is not thread safe - Need to handle multi-thread access contention by locking DbContext
                try
                {
                    lock (_lockerObj)
                    {
                        return DbContext.Products.First(p => p.ProductID == productId);
                    }
                }
                catch (Exception ex)
                {
                    //log
                }

                return null;
            });
        }
    }
}
