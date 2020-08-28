using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tecsys.Retail.Repository
{
    public interface IProductRepository
    {
        Task<List<Ef.Product>> GetCars();
        Task<List<Ef.Product>> GetProducts(string name);
        Task<Ef.Product> GetProduct(int productId);
    }
}
