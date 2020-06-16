using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class MailSubscriber
    {
        [Key]
        public int MailSubscriberId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
