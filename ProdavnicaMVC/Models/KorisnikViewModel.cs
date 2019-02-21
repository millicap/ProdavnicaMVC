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
        [RegularExpression(@"^[\p{L} ]+$", ErrorMessage = "Samo slova i razmak!")]
        public string Ime { get; set; }
        [RegularExpression(@"^[\p{L} ]+$", ErrorMessage = "Samo slova!")]
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        [RegularExpression(@"^M?$|^Z?$", ErrorMessage = "Samo M ili Z!")]
        public string Pol { get; set; }
        public DateTime DatumRodjenja { get; set; }
    }
}