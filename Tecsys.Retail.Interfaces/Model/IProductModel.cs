using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecsys.Retail.Model
{
    public interface IProductModel
    {
        int ProductId { get; set; }
        string ProductName { get; set; }
        double? UnitPrice { get; set; }
        string ImagePath { get; set; }
        string Description { get; set; }
    }
}
