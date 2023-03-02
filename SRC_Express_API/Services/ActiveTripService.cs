using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface ActiveTripService
    {
        public List<ActiveTripsInfo> FindAllActiveTrips();
        public List<ActiveTripsInfo> FindActivetripForPage();
        public ActiveTripsInfo FindActiveTrip(int idactivetrip);
        public List<ActiveTripsInfo> FindAllActiveTripsHasActive();
        public List<ActiveTripsInfo> FindAllActiveTripsHasActive(int idTypecar);
        public Boolean Create(ActiveTrip activetrip);
        public Boolean Update(int Idactivetrip);
        public dynamic FindAllInfoCreate();
        public List<TimeStartInfo> FindTimeStartIn1Trip(int Idtrip);

    }
}
