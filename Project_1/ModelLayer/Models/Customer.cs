using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace ModelLayer
{
    public class Customer
    {
        [Key]
        public Guid Customer_Id { get; set; } = new Guid();

        [StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [StringLength(20, ErrorMessage = "The Last name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Last Name")] 
        public string lastName { get; set; }
        private string FavStore;
        public string favstore
        {
            get { return FavStore; }
            set { FavStore = value != null ? "No where" : value; }
        }
        [Display(Name = "Administrator")]
        public Boolean Addmin { get; set; } = false;
        public byte[] ByteArrayImage { get; set; }

        public Customer()
        {

        }
        public Customer(string fname, string lname)
        {
            firstName = fname;
            lastName = lname;
        }
        public override string ToString()
        {
            return $"{firstName}  {lastName}";
        }
    }
}
