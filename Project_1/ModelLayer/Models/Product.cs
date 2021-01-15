using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace ModelLayer
{
    public class Product
    {
        [Key]
        public int productId {get; set;}
        [Required]
        public string productName {get; set;}
        [Required]
        public double price {get; set;}
        public string description {get; set;}

        public override string ToString()
        {
            return $"{productName} "+$"@ {price}";
        }
    }
}
