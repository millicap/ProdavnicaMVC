using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class RacunViewModel
    {
        public int RacunId { get; set; }
        public int KorisnikId { get; set; }
        public System.DateTime DatumIzdavanja { get; set; }
        public int BrojRacuna { get; set; }
        public decimal UkupanIznos { get; set; }

    }
}