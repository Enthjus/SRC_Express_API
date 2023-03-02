using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class ActiveTripServiceImpl : ActiveTripService

    {
        private DatabaseContext db;
        private TripsService tripsService;
        private CarsService carService;
        public ActiveTripServiceImpl(DatabaseContext _db, TripsService _tripsService, CarsService _carService)
        {
            db = _db;
            tripsService = _tripsService;
            carService = _carService;
        }

        public Boolean Create(ActiveTrip activetrip)
        {
            db.ActiveTrips.Add(activetrip);
            return db.SaveChanges() > 0;

        }

        public List<ActiveTripsInfo> FindAllActiveTrips()
        {
            try
            {
                return db.ActiveTrips.Select(a => new ActiveTripsInfo
                {
                    Id = a.IdtripsTiimeStart,
                    Trip = new TripsInfo
                    {
                        Id = a.IdtripsNavigation.Id,
                        NameTrip = a.IdtripsNavigation.NameTrip,
                        FromAddress = a.IdtripsNavigation.FromAddress,
                        ToAddress = a.IdtripsNavigation.ToAddress,
                        Photo = a.IdtripsNavigation.Photo,
                        Distance = a.IdtripsNavigation.Distance,
                    },
                    TimeStart = new TimeStartInfo
                    {
                        Id = a.IdtimeStartNavigation.IdtimeStart,
                        TimeStart = a.IdtimeStartNavigation.TimeStart1.Value,
                    },
                    Car = new CarsInfo
                    {
                        Id = a.IdcarsNavigation.Id,
                        NameCar = a.IdcarsNavigation.Name,
                        IdType = a.IdcarsNavigation.Type.Value,
                        TypeName = a.IdcarsNavigation.TypeNavigation.TypeName,
                        PercentType = a.IdcarsNavigation.TypeNavigation.PercentType.Value,
                        Model = a.IdcarsNavigation.Model,
                        RegistrationDateStart = a.IdcarsNavigation.RegistrationDateStart.Value,
                        RegistrationDateEnd = a.IdcarsNavigation.RegistrationDateEnd.Value,
                        Photo = a.IdcarsNavigation.Photo
                    },
                    Status = a.Status.Value,
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool Update(int Idactivetrip)
        {
            try
            {
                var activetripUpdate = db.ActiveTrips.Find(Idactivetrip);


                if (activetripUpdate.Status == 0)
                {
                    var activetrip = db.ActiveTrips.Where(at => (at.Idcars.Equals(activetripUpdate.Idcars) && at.Status == 1)||(at.Idtrips==activetripUpdate.Idtrips&&at.IdtimeStart==activetripUpdate.IdtimeStart&&at.Status==1)).Select(at => at).ToList();
                    if (activetrip.Count() == 0)
                    {
                        
                        activetripUpdate.Status = 1;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    var checkTicket = db.Tickets.Where(t => t.IdactiveTrip.Value == Idactivetrip).Select(t => t).ToList();
                    Debug.WriteLine(checkTicket);
                    if (checkTicket.Count() == 0)
                    {

                        activetripUpdate.Status = 0;
                    }
                    else
                    {
                        var checkTicketDate = checkTicket.Where(t => t.StartDate.Value.CompareTo(DateTime.Now) > 0).Select(t => t).ToList();
                        if (checkTicketDate.Count() == 0)
                        {
                            activetripUpdate.Status = 0;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                db.Entry(activetripUpdate).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        public dynamic FindAllInfoCreate()
        {
            var trips = tripsService.FindAllTrip();
            var carHasActive = db.ActiveTrips.Where(a => a.Status == 1).Select(a => a).ToList();
            var cars = carService.FindAllCars().Where(c1 => !carHasActive.Any(c2 => c2.Idcars.Equals(c1.Id)));

            dynamic kq = new ExpandoObject();
            kq.trips = trips;
            kq.cars = cars;
            return kq;
        }

        public List<TimeStartInfo> FindTimeStartIn1Trip(int Idtrip)
        {
            var timestartActive = db.ActiveTrips.Where(a => a.Idtrips.Value == Idtrip && a.Status == 1).Select(a => a).ToList();
            var timestarts = db.TimeStarts.ToList().Where(t1 => !timestartActive.Any(t2 => t2.IdtimeStart.Value == t1.IdtimeStart)).Select(t => new TimeStartInfo
            {
                Id = t.IdtimeStart,
                TimeStart = t.TimeStart1.Value
            }).ToList();
            return timestarts;
        }

        public List<ActiveTripsInfo> FindAllActiveTripsHasActive()
        {
            try
            {
                return db.ActiveTrips.Where(at => at.Status == 1).Select(a => new ActiveTripsInfo
                {
                    Id = a.IdtripsTiimeStart,
                    Trip = new TripsInfo
                    {
                        Id = a.IdtripsNavigation.Id,
                        NameTrip = a.IdtripsNavigation.NameTrip,
                        FromAddress = a.IdtripsNavigation.FromAddress,
                        ToAddress = a.IdtripsNavigation.ToAddress,
                        Photo = a.IdtripsNavigation.Photo,
                        Distance = a.IdtripsNavigation.Distance,
                    },
                    TimeStart = new TimeStartInfo
                    {
                        Id = a.IdtimeStartNavigation.IdtimeStart,
                        TimeStart = a.IdtimeStartNavigation.TimeStart1.Value,
                    },
                    Car = new CarsInfo
                    {
                        Id = a.IdcarsNavigation.Id,
                        NameCar = a.IdcarsNavigation.Name,
                        IdType = a.IdcarsNavigation.Type.Value,
                        TypeName = a.IdcarsNavigation.TypeNavigation.TypeName,
                        PercentType = a.IdcarsNavigation.TypeNavigation.PercentType.Value,
                        Model = a.IdcarsNavigation.Model,
                        RegistrationDateStart = a.IdcarsNavigation.RegistrationDateStart.Value,
                        RegistrationDateEnd = a.IdcarsNavigation.RegistrationDateEnd.Value,
                        Photo = a.IdcarsNavigation.Photo
                    },
                    Status = a.Status.Value,
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<ActiveTripsInfo> FindAllActiveTripsHasActive(int idTypecar)
        {
            try
            {
                return db.ActiveTrips.Where(at => at.Status == 1 && at.IdcarsNavigation.TypeNavigation.Id == idTypecar).Select(a => new ActiveTripsInfo
                {
                    Id = a.IdtripsTiimeStart,
                    Trip = new TripsInfo
                    {
                        Id = a.IdtripsNavigation.Id,
                        NameTrip = a.IdtripsNavigation.NameTrip,
                        FromAddress = a.IdtripsNavigation.FromAddress,
                        ToAddress = a.IdtripsNavigation.ToAddress,
                        Photo = a.IdtripsNavigation.Photo,
                        Distance = a.IdtripsNavigation.Distance,
                    },
                    TimeStart = new TimeStartInfo
                    {
                        Id = a.IdtimeStartNavigation.IdtimeStart,
                        TimeStart = a.IdtimeStartNavigation.TimeStart1.Value,
                    },
                    Car = new CarsInfo
                    {
                        Id = a.IdcarsNavigation.Id,
                        NameCar = a.IdcarsNavigation.Name,
                        IdType = a.IdcarsNavigation.Type.Value,
                        TypeName = a.IdcarsNavigation.TypeNavigation.TypeName,
                        PercentType = a.IdcarsNavigation.TypeNavigation.PercentType.Value,
                        Model = a.IdcarsNavigation.Model,
                        RegistrationDateStart = a.IdcarsNavigation.RegistrationDateStart.Value,
                        RegistrationDateEnd = a.IdcarsNavigation.RegistrationDateEnd.Value,
                        Photo = a.IdcarsNavigation.Photo
                    },
                    Status = a.Status.Value,
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        public ActiveTripsInfo FindActiveTrip(int idactivetrip)
        {
            var a = db.ActiveTrips.SingleOrDefault(at=>at.IdtripsTiimeStart == idactivetrip);
            return new ActiveTripsInfo
            {
                Id = a.IdtripsTiimeStart,
                Trip = new TripsInfo
                {
                    Id = a.IdtripsNavigation.Id,
                    NameTrip = a.IdtripsNavigation.NameTrip,
                    FromAddress = a.IdtripsNavigation.FromAddress,
                    ToAddress = a.IdtripsNavigation.ToAddress,
                    Photo = a.IdtripsNavigation.Photo,
                    Distance = a.IdtripsNavigation.Distance,
                },
                TimeStart = new TimeStartInfo
                {
                    Id = a.IdtimeStartNavigation.IdtimeStart,
                    TimeStart = a.IdtimeStartNavigation.TimeStart1.Value,
                },
                Car = new CarsInfo
                {
                    Id = a.IdcarsNavigation.Id,
                    NameCar = a.IdcarsNavigation.Name,
                    IdType = a.IdcarsNavigation.Type.Value,
                    TypeName = a.IdcarsNavigation.TypeNavigation.TypeName,
                    PercentType = a.IdcarsNavigation.TypeNavigation.PercentType.Value,
                    Model = a.IdcarsNavigation.Model,
                    RegistrationDateStart = a.IdcarsNavigation.RegistrationDateStart.Value,
                    RegistrationDateEnd = a.IdcarsNavigation.RegistrationDateEnd.Value,
                    Photo = a.IdcarsNavigation.Photo
                },
                Status = a.Status.Value,
            };
        }

        public List<ActiveTripsInfo> FindActivetripForPage()
        {
            try
            {
                return db.ActiveTrips.Where(a=>a.Status ==1).Select(a => new ActiveTripsInfo
                {
                    Id = a.IdtripsTiimeStart,
                    Trip = new TripsInfo
                    {
                        Id = a.IdtripsNavigation.Id,
                        NameTrip = a.IdtripsNavigation.NameTrip,
                        FromAddress = a.IdtripsNavigation.FromAddress,
                        ToAddress = a.IdtripsNavigation.ToAddress,
                        Photo = a.IdtripsNavigation.Photo,
                        Distance = a.IdtripsNavigation.Distance,
                    },
                    TimeStart = new TimeStartInfo
                    {
                        Id = a.IdtimeStartNavigation.IdtimeStart,
                        TimeStart = a.IdtimeStartNavigation.TimeStart1.Value,
                    },
                    Car = new CarsInfo
                    {
                        Id = a.IdcarsNavigation.Id,
                        NameCar = a.IdcarsNavigation.Name,
                        IdType = a.IdcarsNavigation.Type.Value,
                        TypeName = a.IdcarsNavigation.TypeNavigation.TypeName,
                        PercentType = a.IdcarsNavigation.TypeNavigation.PercentType.Value,
                        Model = a.IdcarsNavigation.Model,
                        RegistrationDateStart = a.IdcarsNavigation.RegistrationDateStart.Value,
                        RegistrationDateEnd = a.IdcarsNavigation.RegistrationDateEnd.Value,
                        Photo = a.IdcarsNavigation.Photo
                    },
                    Status = a.Status.Value,
                }).Take(5).ToList();
            }
            catch
            {
                return null;
            }
        }
        
    }
}
