using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5032.Models
{
    public class EventType
    {
        [Key]
        public int TypeID { get; set; }

        [Required]
        [MaxLength(30)]
        public string EventName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

    }
}