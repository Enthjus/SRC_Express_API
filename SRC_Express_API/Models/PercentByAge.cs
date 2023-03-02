using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class PercentByAge
    {
        public PercentByAge()
        {
            Totals = new HashSet<Total>();
        }

        public int Id { get; set; }
        public int? AgeStart { get; set; }
        public int? AgeEnd { get; set; }
        public double? PercentDiscount { get; set; }

        public virtual ICollection<Total> Totals { get; set; }
    }
}
