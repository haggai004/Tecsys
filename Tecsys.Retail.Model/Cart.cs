using System.Collections.Generic;

namespace Tecsys.Retail.Model
{
    public class CartModel
    {
        public string CartId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}
