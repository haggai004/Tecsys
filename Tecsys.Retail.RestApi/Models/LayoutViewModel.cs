
using Tecsys.Retail.Model;

namespace Tecsys.Retail.RestApi.Models
{
    public class LayoutViewModel
    {
        public int CartItemCount { get; set; }
        public string ProductSearchText { get; set; }
        public ICartModel CartMode { get; set; }
    }
}