using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment5032.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int RestID { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        [ForeignKey("EventType")]
        public int TypeID { get; set; }
        public EventType EventType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm tt}")]
        public DateTime EndTime { get; set; }
    }
}