using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class FullOrderViewModel
    {
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime dateTime { get; set; }
        public string storeName { get; set; }
        public string location { get; set; }
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public double pricePaid { get; set; }
        public int qtyOrdered { get; set; }
    }
}
