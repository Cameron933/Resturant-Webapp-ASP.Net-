using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment5032.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("RestUser")]
        public int UserId { get; set; }
        public RestUser RestUser { get; set; }

        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}