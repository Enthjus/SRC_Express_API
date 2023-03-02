using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class TicketRequestRefund
    {
        public int Id { get; set; }
        public int? Idticket { get; set; }
        public int? Idrequest { get; set; }

        public virtual RequestRefund IdrequestNavigation { get; set; }
        public virtual Ticket IdticketNavigation { get; set; }
    }
}
