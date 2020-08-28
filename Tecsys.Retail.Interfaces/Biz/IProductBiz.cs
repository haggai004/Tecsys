using System.Collections.Generic;
using System.Threading.Tasks;
using Tecsys.Retail.Domain;

namespace Tecsys.Retail.Biz
{
    public interface IProductService
    {
        Task<IEnumerable<Domain.IProduct>> GetCarsAsync();
        Task<IEnumerable<Domain.IProduct>> GetProductsAsync(string name);
        Task<IProduct> GetProductAsync(int productId);
    }
}
