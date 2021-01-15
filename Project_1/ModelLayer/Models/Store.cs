using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer
{
    public class Store
    {
        public int Id {get; set;}
        [Required]
        public string storeName {get; set;}
        [Required]
        public string location {get; set;}
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double totalSales {get; set;}
        public Store()
        {

        }
        public Store(string sname,string loca)
        {
            storeName = sname;
            location = loca;
        }
        /// <summary>
        /// Adding an item the customer picked into the cart.
        /// </summary>
        /// <param name="i"></param>

        public override string ToString()
        {
            return $"Store number: {Id} {storeName} located in {location}";
        }

        
    }
}
