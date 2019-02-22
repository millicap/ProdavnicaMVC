using ProdavnicaMVC.DBProdavnica;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class KorisnikViewModel : IValidatableObject
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
        public string Username { get; set; }
        public string Password { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)   //okida svaki put kad ima ismodelvalid
        {
            using (var context = new ProdavnicaContext())
            {
                bool zauzetUsername = context.Korisniks.Any(k => k.Username == Username);

                if (zauzetUsername)
                {
                    yield return new ValidationResult("Greska", new[] { nameof(Username) });

                }
               
            }


        }

    }
}