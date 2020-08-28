using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tecsys.Retail.Model
{
    public interface ICartModel
    {
        [Required]
        string CartId { get; set; }
        [Required]
        List<ICartItemModel> CartItemModels { get; set; }
    }
}
