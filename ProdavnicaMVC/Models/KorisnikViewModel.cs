using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class KorisnikViewModel
    {
        public int KorisnikId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Samo slova!")]
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Pol { get; set; }
        public DateTime DatumRodjenja { get; set; }
    }
}