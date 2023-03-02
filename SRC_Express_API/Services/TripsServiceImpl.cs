using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class TripsServiceImpl : TripsService
    {
        private DatabaseContext db;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        public TripsServiceImpl(DatabaseContext _db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }
        public List<TripsInfo> FindAllTrip()
        {
            return db.Trips.Select(t => new TripsInfo
            {
                Id = t.Id,
                NameTrip = t.NameTrip,
                FromAddress = t.FromAddress,
                ToAddress = t.ToAddress,
                Photo = t.Photo,
                Distance = t.Distance
            }).ToList();
        }

        public TripsInfo FindTripbyIDTrip(int idtrip)
        {
            var t = db.Trips.SingleOrDefault(t => t.Id == idtrip);
            return new TripsInfo
            {
                Id = t.Id,
                NameTrip = t.NameTrip,
                FromAddress = t.FromAddress,
                ToAddress = t.ToAddress,
                Photo = t.Photo,
                Distance = t.Distance
            };
        }

        public TripsInfo CreateTrip(TripsInfo tripInfo)
        {
            var newTrip = new Trip
            {
                NameTrip = tripInfo.NameTrip,
                FromAddress = tripInfo.FromAddress,
                ToAddress = tripInfo.ToAddress,
                Photo = tripInfo.Photo,
                Distance = tripInfo.Distance
            };
            db.Trips.Add(newTrip);
            db.SaveChanges();
            return new TripsInfo
            {
                Id = newTrip.Id,
                NameTrip = newTrip.NameTrip,
                FromAddress = newTrip.FromAddress,
                ToAddress = newTrip.ToAddress,
                Photo = newTrip.Photo,
                Distance = newTrip.Distance
            };
        }

        public bool DeleteTrip(int idtrip)
        {
            db.Remove(db.Trips.Find(idtrip));
            return db.SaveChanges() > 0;
        }

        public TripsInfo UpdateTrip(TripsInfo tripInfo)
        {
            var updatetrip = db.Trips.SingleOrDefault(t => t.Id == tripInfo.Id);
            updatetrip.NameTrip = tripInfo.NameTrip;
            updatetrip.FromAddress = tripInfo.FromAddress;
            updatetrip.ToAddress = tripInfo.ToAddress;
            updatetrip.Distance = tripInfo.Distance;

            updatetrip.Photo = tripInfo.Photo;
            db.Entry(updatetrip).State = EntityState.Modified;
            db.SaveChanges();
            return new TripsInfo
            {
                Id = updatetrip.Id,
                NameTrip = updatetrip.NameTrip,
                FromAddress = updatetrip.FromAddress,
                ToAddress = updatetrip.ToAddress,
                Photo = updatetrip.Photo,
                Distance = updatetrip.Distance
            };



        }

        public bool DeletePhoto(int idtrip)
        {
            try
            {
                var tripdelete = db.Trips.SingleOrDefault(t => t.Id == idtrip);
                if (tripdelete.Photo.Equals("http://localhost:57771/uploads/Inazuma_City.png"))
                {
                    return true;
                }
                else
                {
                    var photodelete = tripdelete.Photo.Replace("http://localhost:57771/uploads/", "");
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", photodelete);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch
            {
                return false;
            }


        }
    }
}
