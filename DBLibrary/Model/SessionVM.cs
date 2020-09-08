using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class SessionVM
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
    }
}