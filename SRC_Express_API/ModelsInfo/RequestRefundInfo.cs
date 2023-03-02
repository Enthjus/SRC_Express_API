using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class RequestRefundInfo
    {
        public int Idrequest { get; set; }
        public int IdTicket { get; set; }
        public double? PercentRefund { get; set; }
        public double? TotalRefund { get; set; }
        public int? StatusDone { get; set; }
        public DateTime? DaysSendRefund { get; set; }
        public DateTime? DaysConfirmRefund { get; set; }
    }
}
