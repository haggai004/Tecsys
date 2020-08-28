using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tecsys.Retail.Domain
{
    public interface ICart
    {
        string CartId { get; set; }
        [Required]
        List<ICartItem> CartItems { get; set; }

    }
}
