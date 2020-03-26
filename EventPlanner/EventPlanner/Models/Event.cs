using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Event name")]
        public string EventName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Visitor Limit")]
        [Range (1, 10000)]
        public int VisitorLimit { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please use a valid e-mailadress")]
        public string Email { get; set; }

        [Required]
        public string ImageSrc { get; set; }
    }
}