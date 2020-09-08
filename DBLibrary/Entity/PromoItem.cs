namespace DBLibrary.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PromoItem
    {
        public Guid PromoItemId { get; set; }

        public Guid ItemId { get; set; }

        public int PromotionalQty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal FixedPrice { get; set; }

        public int? GroupId { get; set; }

        public virtual Item Item { get; set; }
    }
}
