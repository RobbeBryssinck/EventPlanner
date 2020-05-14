using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class EventViewModel
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Voer de naam van het evenement in")]
        [StringLength(250)]
        [Display(Name = "Evenement naam")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Datum")]
        [Range(typeof(DateTime), "15/05/2020", "31/12/2100",
            ErrorMessage = "Ingevoerde datum moet tussen 15/05/2020 en 31/12/2100 zijn")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Stel een bezoekers limiet in 0-1000")]
        [Display(Name = "Bezoeker limiet")]
        [Range(1, 10000, ErrorMessage = "Stel een bezoekers limiet in 0-1000")]
        public int VisitorLimit { get; set; }

        [Required(ErrorMessage = "Voer een beschrijving in van het evenement")]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Voer geldige locatie in van het evenement")]
        [Display(Name = "Locatie")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Voer een geldig emailadres in")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please use a valid e-mailadress")]
        public string Email { get; set; }
        public string ImageSrc { get; set; }

        [Required]
        [Display(Name = "Evenement type")]
        public string EventType { get; set; }
        public int Visitors { get; set; }

        [Required(ErrorMessage = "Upload een foto voor het evenement")]
        [Display(Name = "Foto")]
        public ICollection<IFormFile> files { get; set; }

        [Required]
        [Display(Name = "Voor wie?")]
        public EventGroup ForEmployees { get; set; }

        public List<Categorie> Categories { get; set; }
    }
}