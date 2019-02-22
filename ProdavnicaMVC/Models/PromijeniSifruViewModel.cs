using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class PromijeniSifruViewModel
    {
        public int KorisnikId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [CompareAttribute("Password")]
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [CompareAttribute("NewPassword")]
        public string NewPasswordRepeated { get; set; }

    }
}