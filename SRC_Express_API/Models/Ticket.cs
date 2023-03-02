using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketRequestRefunds = new HashSet<TicketRequestRefund>();
        }

        public int Id { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string IdAccCustomer { get; set; }
        public double? Total { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Creater { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Age { get; set; }
        public int? IdactiveTrip { get; set; }
        public string Seats { get; set; }

        public virtual Account IdAccCustomerNavigation { get; set; }
        public virtual ActiveTrip IdactiveTripNavigation { get; set; }
        public virtual ICollection<TicketRequestRefund> TicketRequestRefunds { get; set; }
    }
}
