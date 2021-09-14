using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5032.Models
{
    public class Restaurant
    {
        [Key]
        public int RestID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(9)]
        public string ContactNumb { get; set; }
    }
}