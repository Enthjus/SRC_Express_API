using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class PricePerKmServiceImpl : PricePerKmService
    {
        private DatabaseContext db;

        public PricePerKmServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }
        public List<PricePerKmInfo> FindAllPricePerKm()
        {
            return db.PricePerKms.Select(p => new PricePerKmInfo
            {
                Id = p.Id,
                Price = p.Price,
            }).ToList();
        }

        public PricePerKmInfo FindPricePerKm(int idPrice)
        {
            var price = db.PricePerKms.SingleOrDefault(p => p.Id == idPrice);
            return new PricePerKmInfo
            {
                Id = price.Id,
                Price = price.Price.Value,
            };
        }

        public PricePerKmInfo UpdatePricePerKm(PricePerKmInfo pricePerKmInfo)
        {
            var priceUpdate = db.PricePerKms.SingleOrDefault(p => p.Id == pricePerKmInfo.Id);
            priceUpdate.Price = pricePerKmInfo.Price;
            db.Entry(priceUpdate).State = EntityState.Modified;
            db.SaveChanges();
            return new PricePerKmInfo
            {
                Id = priceUpdate.Id,
                Price = pricePerKmInfo.Price,
            };
        }
    }
}
