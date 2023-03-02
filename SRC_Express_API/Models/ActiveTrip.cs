using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class ActiveTrip
    {
        public ActiveTrip()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdtripsTiimeStart { get; set; }
        public int? Idtrips { get; set; }
        public int? IdtimeStart { get; set; }
        public string Idcars { get; set; }
        public int? Status { get; set; }

        public virtual Car IdcarsNavigation { get; set; }
        public virtual TimeStart IdtimeStartNavigation { get; set; }
        public virtual Trip IdtripsNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
