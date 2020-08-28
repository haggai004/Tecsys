using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tecsys.Retail.MvcWeb.Models
{
    [Serializable]
    [DataContract]
    public class LayoutModel
    {
        [Required(ErrorMessage = "Search text is required")]
        [MinLength(2, ErrorMessage = "Search text must be at least {1} characters long")]
        [DataMember]
        public string SearchText { get; set; } = "";
        [DataMember]
        public int CartItemsCount { get; set; } = 0;
        public int ModelItemsCount { get; set; } = 0;

        public string ApplicationTitle { get; set; }
    }
}