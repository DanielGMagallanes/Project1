using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
    public class Orders
    {
        [Key]
        public int orderID { get; set; }
        [Required]
        public int storeLocationID { get; set; }
        [Required]
        public string customerGuid { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double total { get; set; }
        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime dateTime { get; set; }

        public override string ToString()
        {
            return $"Ordered at {storeLocationID}\nTotal was {total}\nat {dateTime}";
        }
    }
}
