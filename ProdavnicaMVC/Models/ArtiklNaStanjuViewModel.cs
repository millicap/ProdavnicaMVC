using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class ArtiklNaStanjuViewModel
    {
        [Display(Name = "ArtiklId", ResourceType = typeof(Resources.Resource))]
        public int ArtiklId { get; set; }
        [Display(Name = "Naziv", ResourceType = typeof(Resources.Resource))]
        [Required]
        // [RegularExpression(@"^[\p{L} 0-9]+$", ErrorMessage = "Samo slova i razmak!")]
        public string Naziv { get; set; }
        [Display(Name = "Cijena", ResourceType = typeof(Resources.Resource))]
        public decimal Cijena { get; set; }
        [Display(Name = "Kolicina", ResourceType = typeof(Resources.Resource))]
        public int Kolicina { get; set; }
    }
}