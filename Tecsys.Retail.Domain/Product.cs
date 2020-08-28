

using System.ComponentModel.DataAnnotations;

namespace Tecsys.Retail.Domain
{
    public class Product : IProduct
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        public double? UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}
