using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProdavnicaMVC.Models
{
    public class StavkaViewModel
    {
        public int StavkaId { get; set; }
        public int RacunId { get; set; }
        public int ArtiklId { get; set; }
        public decimal Cijena { get; set; }
        public decimal Kolicina { get; set; }
    }
}