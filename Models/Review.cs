using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspWebTest2.Models;

namespace AspWebTest2.Models
{
    public class Review
    {
        [Key]
        public int ReviewNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerID { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [Required]
        [StringLength(255)]
        public string ReviewDetails { get; set; }

        public int? ReviewRatings { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
    }
}