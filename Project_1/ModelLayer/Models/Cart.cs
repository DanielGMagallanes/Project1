using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer
{
    public class Cart
    {
        [Key]
        public int id { get; set; }
        [Column("Location")]
        public int the_store_id { get; set; }
        [Column("Owner_Id")]
        public string customerGuild { get; set; }
        [Column("Cart")]
        public int InShoppingCart { get; set; }
        public int amountPicked { get; set; }
        
        public Cart() { }

    }
}
