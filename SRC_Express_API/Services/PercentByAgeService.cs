using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface PercentByAgeService
    {
        public List<PercentByAgeInfo> FindAllPercentByAge();
        public PercentByAgeInfo FindPercentByAge(int idPrice);
        public PercentByAgeInfo UpdatePercentByAge(PercentByAgeInfo pricePerKmInfo);
    }
}
