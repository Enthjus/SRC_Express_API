using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class TicketInfo
    {
        public int Id { get; set; }
        public AccountInfo IdAccCustomer { get; set; }
        public double Total { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Creater { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Age { get; set; }
        public ActiveTripsInfo IdactiveTrip { get; set; }
        public string Seats { get; set; }
    }
}
