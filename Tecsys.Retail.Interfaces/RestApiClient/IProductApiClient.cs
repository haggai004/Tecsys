using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tecsys.Retail.Model;


namespace Tecsys.Retail.RestApiClient
{
    public interface IProductApiClient
    {
        Task<IEnumerable<IProductModel>> GetCars();

        Task<IEnumerable<IProductModel>> GetProducts(string name);
    }
}
