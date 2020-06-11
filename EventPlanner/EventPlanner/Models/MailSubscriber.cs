using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class MailSubscriber
    {
        [Key]
        public int MailSubscriberId { get; set; }
        public string Email { get; set; }
    }
}
