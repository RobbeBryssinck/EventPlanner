<<<<<<< HEAD
﻿using System;
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
        public string EventName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int VisitorLimit { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter a vaid e-mailadres")]
        public string Email { get; set; }

        [Required]
        public string ImageSrc { get; set; }
    }
}
=======
﻿using System;
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

        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public int VisitorLimit { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string ImageSrc { get; set; }
    }
}
>>>>>>> master
