
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Tecsys.Retail.Converters;

namespace Tecsys.Retail.Model
{
    public class CartItemModel:ICartItemModel
    {
        [Required]
        [MaxLength(128)]
        public string ItemId { get; set; }
        public string CartId { get; set; }
        [Required]
        [Range(1,100,ErrorMessage = "Valid numbers are in the range is 1 - 100")]
        public int Quantity { get; set; }
        [Required]
        public System.DateTime DateCreated { get; set; }
        [Required]
        public int ProductId { get; set; }

        [JsonConverter(typeof(JsonInterfaceConverter<ProductModel>))]
        public IProductModel ProductModel { get; set; }
    }
}
