
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Tecsys.Retail.Converters;

namespace Tecsys.Retail.Domain
{
    public class CartItem: ICartItem
    {
        [Required]
        [MaxLength(128)]
        public string ItemId { get; set; }
        public string CartId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public System.DateTime DateCreated { get; set; }
        [Required]
        public int ProductId { get; set; }

        //Attribute handles interface serialization
        [JsonConverter(typeof(JsonInterfaceConverter<Product>))]
        public IProduct Product { get; set; }
    }
}
