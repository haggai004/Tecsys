using System;

using System.ComponentModel.DataAnnotations;

namespace Tecsys.Retail.Model
{
    public interface ICartItemModel
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
        IProductModel ProductModel { get; set; }
    }
}
