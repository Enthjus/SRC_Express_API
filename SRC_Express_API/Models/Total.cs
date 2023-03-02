using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Total
    {
        public int Idtotal { get; set; }
        public int? IdPercentByAge { get; set; }
        public int? IdPricePerKm { get; set; }
        public int? IdtypeCar { get; set; }
        public double? Total1 { get; set; }

        public virtual PercentByAge IdPercentByAgeNavigation { get; set; }
        public virtual PricePerKm IdPricePerKmNavigation { get; set; }
        public virtual TypeCar IdtypeCarNavigation { get; set; }
    }
}
