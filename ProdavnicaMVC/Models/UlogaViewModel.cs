using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class UlogaViewModel
    {
        public short UlogaId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Samo slova!")]
        public string Naziv { get; set; }
    }
}