using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Trip
    {
        public Trip()
        {
            ActiveTrips = new HashSet<ActiveTrip>();
        }

        public int Id { get; set; }
        public string NameTrip { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Photo { get; set; }
        public double? Distance { get; set; }

        public virtual ICollection<ActiveTrip> ActiveTrips { get; set; }
    }
}
