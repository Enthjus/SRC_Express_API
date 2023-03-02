using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class PercentByAgeServiceImpl : PercentByAgeService
    {
        private DatabaseContext db;
        public PercentByAgeServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public List<PercentByAgeInfo> FindAllPercentByAge()
        {
            return db.PercentByAges.Select(p => new PercentByAgeInfo
            {
                Id = p.Id,
                AgeStart = p.AgeStart,
                AgeEnd = p.AgeEnd,
                PercentDiscount = p.PercentDiscount
            }).ToList();
        }

        public PercentByAgeInfo FindPercentByAge(int idPrice)
        {
            var price = db.PercentByAges.SingleOrDefault(p => p.Id == idPrice);
            return new PercentByAgeInfo
            {
                Id = price.Id,
                AgeStart = price.AgeStart,
                AgeEnd = price.AgeEnd,
                PercentDiscount = price.PercentDiscount
            };
        }

        public PercentByAgeInfo UpdatePercentByAge(PercentByAgeInfo percentbyageInfo)
        {
            var priceUpdate = db.PercentByAges.SingleOrDefault(p => p.Id == percentbyageInfo.Id);
            priceUpdate.AgeStart = percentbyageInfo.AgeStart;
            priceUpdate.AgeEnd = percentbyageInfo.AgeEnd;
            priceUpdate.PercentDiscount = percentbyageInfo.PercentDiscount;
            db.Entry(priceUpdate).State = EntityState.Modified;
            db.SaveChanges();
            return new PercentByAgeInfo
            {
                Id = priceUpdate.Id,
                AgeStart = priceUpdate.AgeStart,
                AgeEnd = priceUpdate.AgeEnd,
                PercentDiscount = priceUpdate.PercentDiscount
            };
        }
    }
}
