

namespace Tecsys.Retail.Domain
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string ProductName { get; set; }
        double? UnitPrice { get; set; }
        string ImagePath { get; set; }
        int CategoryId { get; set; }
        string Description { get; set; }
    }
}
