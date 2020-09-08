using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary.Model
{
    public class PromoItemsVM
    {
        public Guid PromoItemId { get; set; }
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public int PromotionalQty { get; set; }
        public decimal FixedPrice { get; set; }
        public int GroupId { get; set; }

    }
}
