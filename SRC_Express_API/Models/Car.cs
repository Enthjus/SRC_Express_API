using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Car
    {
        public Car()
        {
            ActiveTrips = new HashSet<ActiveTrip>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int? Type { get; set; }
        public string Model { get; set; }
        public DateTime? RegistrationDateStart { get; set; }
        public DateTime? RegistrationDateEnd { get; set; }
        public string Photo { get; set; }

        public virtual TypeCar TypeNavigation { get; set; }
        public virtual ICollection<ActiveTrip> ActiveTrips { get; set; }
    }
}
