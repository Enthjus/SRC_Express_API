using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class RequestRefund
    {
        public RequestRefund()
        {
            TicketRequestRefunds = new HashSet<TicketRequestRefund>();
        }

        public int Idrequest { get; set; }
        public int? Idrefund { get; set; }
        public double? TotalRefund { get; set; }
        public int? StatusDone { get; set; }
        public DateTime? DaysSendRefund { get; set; }
        public DateTime? DaysConfirmRefund { get; set; }

        public virtual Refund IdrefundNavigation { get; set; }
        public virtual ICollection<TicketRequestRefund> TicketRequestRefunds { get; set; }
    }
}
