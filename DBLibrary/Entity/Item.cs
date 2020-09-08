namespace DBLibrary.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            PromoItems = new HashSet<PromoItem>();
        }

        public Guid ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UnitPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PromoItem> PromoItems { get; set; }
    }
}
