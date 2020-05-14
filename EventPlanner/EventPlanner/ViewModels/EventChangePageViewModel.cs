using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class EventChangePageViewModel 
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Evenement naam")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Bezoeker limiet")]
        [Range(1, 10000)]
        public int VisitorLimit { get; set; }

        [Required]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Locatie")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please use a valid e-mailadress")]
        public string Email { get; set; }
        public string ImageSrc { get; set; }

        [Required]
        [Display(Name = "Evenement type")]
        public string EventType { get; set; }
        public int Visitors { get; set; }

        [Display(Name = "Foto")]
        public ICollection<IFormFile> files { get; set; }

        [Required]
        [Display(Name = "Voor wie?")]
        public EventGroup ForEmployees { get; set; }

        public List<Categorie> Categories { get; set; }
    }
}