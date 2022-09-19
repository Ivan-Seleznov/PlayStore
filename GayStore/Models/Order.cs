using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PlayStore.Models
{
    public class Order
       
    {
        public bool Shipped { get; set; }   
        [BindNever]
        public int OrderId { get; set; } 
        [BindNever]
        public ICollection<CartLine>? Lines { get; set; } 
        
        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }

        [Required(ErrorMessage ="Please enter a city name")]
        public string City { get; set; }

        public string? Zip { get; set; }
        public string? State { get; set; }

        public bool GiftWrap { get; set; }


    }
}
