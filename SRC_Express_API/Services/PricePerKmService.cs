using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface PricePerKmService
    {
        public List<PricePerKmInfo> FindAllPricePerKm();
        public PricePerKmInfo FindPricePerKm(int idPrice);
        public PricePerKmInfo UpdatePricePerKm(PricePerKmInfo pricePerKmInfo);
    }
}
