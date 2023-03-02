using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class CarsServiceImpl : CarsService
    {
        private DatabaseContext db;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;
        public CarsServiceImpl(DatabaseContext _db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        public CarsInfo CreateCars(CarsInfo carInfo)
        {
            try
            {
                var IDCar = getTrueRandomID();
                var newCar = new Car
                {
                    Id = IDCar,
                    Name = carInfo.NameCar,
                    Type = carInfo.IdType,
                    Model = carInfo.Model,
                    RegistrationDateStart = carInfo.RegistrationDateStart,
                    RegistrationDateEnd = carInfo.RegistrationDateEnd,
                    Photo = carInfo.Photo
                };
                var typecar = db.TypeCars.SingleOrDefault(t => t.Id == newCar.Type);
                //Debug.WriteLine(newCar.Id);
                //Debug.WriteLine(newCar.Type);
                //Debug.WriteLine(newCar.Name);
                //Debug.WriteLine(newCar.Model);
                //Debug.WriteLine(newCar.RegistrationDateStart);
                //Debug.WriteLine(newCar.RegistrationDateEnd);
                //Debug.WriteLine(newCar.Photo);
                //Debug.WriteLine(typecar.TypeName);
                //Debug.WriteLine(typecar.PercentType);
                db.Cars.Add(newCar);
                db.SaveChanges();
                return new CarsInfo
                {
                    Id = newCar.Id,
                    NameCar = newCar.Name,
                    IdType = newCar.Type.Value,
                    TypeName = typecar.TypeName,
                    PercentType = typecar.PercentType.Value,
                    Model = newCar.Model,
                    RegistrationDateStart = newCar.RegistrationDateStart.Value,
                    RegistrationDateEnd = newCar.RegistrationDateEnd.Value,
                    Photo = newCar.Photo
                };
            }
            catch
            {
                return null;
            }
        }

        public CarsInfo UpdateCars(CarsInfo carInfo)
        {
            var updateCar = db.Cars.SingleOrDefault(c => c.Id.Equals(carInfo.Id));
            updateCar.Name = carInfo.NameCar;
            updateCar.Type = carInfo.IdType;
            updateCar.Model = carInfo.Model;
            updateCar.RegistrationDateStart = carInfo.RegistrationDateStart.Value;
            updateCar.RegistrationDateEnd = carInfo.RegistrationDateEnd.Value;
            updateCar.Photo = carInfo.Photo;
            db.Entry(updateCar).State = EntityState.Modified;
            db.SaveChanges();
            var typecar = db.TypeCars.SingleOrDefault(t => t.Id == updateCar.Type);
            return new CarsInfo
            {
                Id = updateCar.Id,
                NameCar = updateCar.Name,
                IdType = updateCar.Type.Value,
                TypeName = typecar.TypeName,
                PercentType = typecar.PercentType.Value,
                Model = updateCar.Model,
                RegistrationDateStart = updateCar.RegistrationDateStart.Value,
                RegistrationDateEnd = updateCar.RegistrationDateEnd.Value,
                Photo = updateCar.Photo
            };
        }

        public List<CarsInfo> FindAllCars()
        {
            return db.Cars.Select(c => new CarsInfo
            {
                Id = c.Id,
                NameCar = c.Name,
                IdType = c.Type.Value,
                TypeName = c.TypeNavigation.TypeName,
                PercentType = c.TypeNavigation.PercentType,
                Model = c.Model,
                RegistrationDateStart = c.RegistrationDateStart,
                RegistrationDateEnd = c.RegistrationDateEnd,
                Photo = c.Photo
            }).ToList();
        }

        public CarsInfo FindCarbyID(string idcar)
        {
            var Cars = db.Cars.SingleOrDefault(c => c.Id.Equals(idcar));
            return new CarsInfo
            {
                Id = Cars.Id,
                NameCar = Cars.Name,
                IdType = Cars.Type.Value,
                TypeName = Cars.TypeNavigation.TypeName,
                PercentType = Cars.TypeNavigation.PercentType,
                Model = Cars.Model,
                RegistrationDateStart = Cars.RegistrationDateStart,
                RegistrationDateEnd = Cars.RegistrationDateEnd,
                Photo = Cars.Photo
            };
        }

        public bool DeleteCars(string idcars)
        {
            var activecar = db.ActiveTrips.SingleOrDefault(i => i.Idcars.Equals(idcars) && i.Status == 1);
            if (activecar == null){
                db.Remove(db.Cars.SingleOrDefault(c => c.Id.Equals(idcars)));
                return db.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
           
        }
        public bool DeletePhoto(string idcar)
        {
            try
            {
                var cardelete = db.Cars.SingleOrDefault(t => t.Id.Equals(idcar));
                if (cardelete.Photo.Equals("http://localhost:57771/uploads/ship.jpg"))
                {
                    return true;
                }
                else
                {
                    var photodelete = cardelete.Photo.Replace("http://localhost:57771/uploads/", "");
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

        public dynamic FindTypeCarByIDtype(int id)
        {
            var type = db.TypeCars.SingleOrDefault(t => t.Id == id);
            dynamic kq = new ExpandoObject();
            kq.idType = type.Id;
            kq.nameType = type.TypeName;
            kq.PercentType = type.PercentType;
            return kq;
        }


        //============= helper
        public string getTrueRandomID()
        {
            var randomNumID = "Car" + RandomNumber(6);
            var checkid = db.Cars.SingleOrDefault(a => a.Id.Equals(randomNumID));
            if (checkid == null)
            {
                return randomNumID;
            }
            else
            {
                getTrueRandomID();
                return "";
            }
        }

        public static string RandomNumber(int numberRD)
        {
            string randomStr = "";
            try
            {

                int[] myIntArray = new int[numberRD];
                int x;
                //that is to create the random # and add it to uour string
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception ex)
            {
                randomStr = "error";
            }
            return randomStr;
        }


    }
}
