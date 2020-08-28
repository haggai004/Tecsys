

using System.ComponentModel.DataAnnotations;

namespace Tecsys.Retail.Model
{
    public class ProductModel : IProductModel
    {
        [Required]
        public int ProductId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        public double? UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }
}
