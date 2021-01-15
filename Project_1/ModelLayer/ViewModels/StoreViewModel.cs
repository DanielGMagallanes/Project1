using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class StoreViewModel
    {
        public int productId { get; set; }
        [Display(Name = "Product Name")]
        public string productName { get; set; }
        [Display(Name = "Price")]
        public double price { get; set; }
        [Display(Name = "Quantity Available")]
        public int qty { get; set; }
        public int ID_store { get; set; }
        public double sale { get; set; }
    }
}
