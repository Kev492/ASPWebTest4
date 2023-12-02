// Models/Membership.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace AspWebTest2.Models
{
    public class Membership
    {
        [Key]
        public string CustomerID { get; set; }
        
        [Required]
        [Display(Name = "Payment Account")]
        public string PaymentAccount { get; set; }

        [Required]
        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        // Add other membership-related properties as needed
    }
}