using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class OrderViewModel
    {
        public int orderID { get; set; }

        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime dateTime { get; set; }
        [Range(typeof(decimal), "0", "2")]
        public double total { get; set; }
 
    }
}
