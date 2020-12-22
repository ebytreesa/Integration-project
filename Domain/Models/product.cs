using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        public int ninjaProductId { get; set; }
        public string econProductId { get; set; }
        public string sourceId { get; set; }
        public string  source { get; set; }
        public string  name { get; set; }
        public string description { get; set; }
        public float recommendedPrice { get; set; }
        public float salesPrice { get; set; }
        public DateTime lastUpdated { get; set; }
        public DateTime dbUpdatedAt { get; set; }
    }
}
