using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Customer_Id { get; set; } = new Guid();

        [StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        public IFormFile IformFileImage { get; set; }
        public string JpgStringImage { get; set; }
    }
}
