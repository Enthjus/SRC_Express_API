using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Refund
    {
        public Refund()
        {
            RequestRefunds = new HashSet<RequestRefund>();
        }

        public int Id { get; set; }
        public int? DayToStart { get; set; }
        public double? RefundPercent { get; set; }

        public virtual ICollection<RequestRefund> RequestRefunds { get; set; }
    }
}
