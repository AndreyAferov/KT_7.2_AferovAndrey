//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterFloor.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adress()
        {
            this.Partner = new HashSet<Partner>();
        }
    
        public int Id { get; set; }
        public int IndexCountry { get; set; }
        public int Region { get; set; }
        public int Country { get; set; }
        public int Street { get; set; }
        public int HouseNum { get; set; }
    
        public virtual CountryIndex CountryIndex { get; set; }
        public virtual Countrys Countrys { get; set; }
        public virtual Regions Regions { get; set; }
        public virtual Regions Regions1 { get; set; }
        public virtual Streets Streets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partner> Partner { get; set; }
    }
}
