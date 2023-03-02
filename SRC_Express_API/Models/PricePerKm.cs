using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class PricePerKm
    {
        public PricePerKm()
        {
            Totals = new HashSet<Total>();
        }

        public int Id { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Total> Totals { get; set; }
    }
}
