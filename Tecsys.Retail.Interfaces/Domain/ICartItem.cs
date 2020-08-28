
using System.ComponentModel.DataAnnotations;

namespace Tecsys.Retail.Domain
{
    public interface ICartItem
    {
        [Required]
        string ItemId { get; set; }
        string CartId { get; set; }
        [Required]
        int Quantity { get; set; }
        [Required]
        System.DateTime DateCreated { get; set; }
        [Required]
        int ProductId { get; set; }
        IProduct Product { get; set; }
    }
}
