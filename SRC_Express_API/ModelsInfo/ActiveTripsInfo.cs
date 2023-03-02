using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class ActiveTripsInfo
    {
        public int Id { get; set; }
        public TripsInfo Trip { get; set; }
        public  TimeStartInfo TimeStart { get; set; }
        public CarsInfo Car { get; set; }
        public int Status { get; set; }

    }
}
