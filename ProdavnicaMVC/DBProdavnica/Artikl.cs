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
    
    public partial class Artikl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artikl()
        {
            this.Stavkas = new HashSet<Stavka>();
        }
    
        public int ArtiklId { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public int DobavljacId { get; set; }
        public string Slika { get; set; }
    
        public virtual Dobavljac Dobavljac { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stavka> Stavkas { get; set; }
    }
}
