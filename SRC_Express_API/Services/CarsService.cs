using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface CarsService
    {
        public List<CarsInfo> FindAllCars();
        public CarsInfo FindCarbyID(string idcar);
        public CarsInfo CreateCars(CarsInfo carInfo);
        public CarsInfo UpdateCars(CarsInfo carInfo);
        public Boolean DeleteCars(string idcars);
        public Boolean DeletePhoto(string idcar);
        public dynamic FindTypeCarByIDtype(int id);
    }
}
