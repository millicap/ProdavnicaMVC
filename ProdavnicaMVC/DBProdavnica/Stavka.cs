//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProdavnicaMVC.DBProdavnica
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stavka
    {
        public int StavkaId { get; set; }
        public int RacunId { get; set; }
        public int ArtiklId { get; set; }
        public decimal Cijena { get; set; }
        public decimal Kolicina { get; set; }
    
        public virtual Artikl Artikl { get; set; }
        public virtual Racun Racun { get; set; }
    }
}
