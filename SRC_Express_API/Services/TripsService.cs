using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface TripsService
    {
        public List<TripsInfo> FindAllTrip();
        public TripsInfo FindTripbyIDTrip(int idtrip);
        public TripsInfo CreateTrip(TripsInfo tripInfo);
        public TripsInfo UpdateTrip(TripsInfo tripInfo);
        public Boolean DeleteTrip(int idtrip);
        public Boolean DeletePhoto(int idtrip);
    }
}
