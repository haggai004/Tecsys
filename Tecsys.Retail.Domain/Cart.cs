using System.Collections.Generic;


namespace Tecsys.Retail.Domain
{
    public class Cart: ICart
    {
        public Cart()
        {
            CartItems = new List<ICartItem>();
        }
        public string CartId { get; set; }
        public List<ICartItem> CartItems { get; set; }

    }
}
