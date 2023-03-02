using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class TypeCar
    {
        public TypeCar()
        {
            Cars = new HashSet<Car>();
            Totals = new HashSet<Total>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public double? PercentType { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Total> Totals { get; set; }
    }
}
