using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class TimeStart
    {
        public TimeStart()
        {
            ActiveTrips = new HashSet<ActiveTrip>();
        }

        public int IdtimeStart { get; set; }
        public DateTime? TimeStart1 { get; set; }

        public virtual ICollection<ActiveTrip> ActiveTrips { get; set; }
    }
}
