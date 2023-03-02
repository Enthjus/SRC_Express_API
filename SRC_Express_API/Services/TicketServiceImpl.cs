using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace SRC_Express_API.Services
{
    public class TicketServiceImpl : TicketService
    {
        private DatabaseContext db;
        private IWebHostEnvironment webHostEnvironment;
        private IHttpContextAccessor httpContextAccessor;

        private static string host = "smtp.gmail.com";
        private static int port = 587;
        private static bool enableSsl = true;
        private static string email = "tranthanhtungdnhhtb@gmail.com";
        private static string password = "anhlaphuc123";
        public TicketServiceImpl(DatabaseContext _db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
        {
            db = _db;
            webHostEnvironment = _webHostEnvironment;
            httpContextAccessor = _httpContextAccessor;
        }

        public TicketInfo CreateTicket(Ticket tk)
        {
            try
            {
                var check = db.Tickets.SingleOrDefault(t => t.IdactiveTrip == tk.IdactiveTrip && t.Seats.Equals(tk.Seats) && tk.StartDate.Value.Year == 1900);
                if(check == null)
                {
                    db.Tickets.Add(tk);
                    db.SaveChanges();
                    var tknew = db.Tickets.Find(tk.Id);
                    return new TicketInfo
                    {
                        Id = tknew.Id,
                        IdAccCustomer = new AccountInfo
                        {
                            Id = tknew.IdAccCustomer,
                            Username = db.Accounts.SingleOrDefault(c => c.Id.Equals(tknew.IdAccCustomer)).Username,
                            Password = "",
                            FullName = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(tknew.IdAccCustomer)).FullName,
                            Email = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(tknew.IdAccCustomer)).Email,
                            Dob = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(tknew.IdAccCustomer)).Dob,
                            Photo = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(tknew.IdAccCustomer)).Photo,
                            NameRole = tknew.IdAccCustomerNavigation.IdroleNavigation.Name,
                        },
                        Total = tknew.Total.Value,
                        CreateDate = tknew.CreateDate,
                        Creater = tknew.Creater,
                        StartDate = tknew.StartDate,
                        Age = tknew.Age,
                        IdactiveTrip = new ActiveTripsInfo
                        {
                            Id = db.ActiveTrips.SingleOrDefault(c => c.IdtripsTiimeStart == tknew.IdactiveTrip.Value).IdtripsTiimeStart,
                            Trip = new TripsInfo
                            {
                                Id = tknew.IdactiveTripNavigation.Idtrips.Value,
                                NameTrip = db.Trips.SingleOrDefault(c => c.Id == tknew.IdactiveTripNavigation.Idtrips.Value).NameTrip,
                                FromAddress = tknew.FromAddress,
                                ToAddress = tknew.ToAddress,
                                Photo = db.Trips.SingleOrDefault(c => c.Id == tknew.IdactiveTripNavigation.Idtrips.Value).Photo,
                                Distance = db.Trips.SingleOrDefault(c => c.Id == tknew.IdactiveTripNavigation.Idtrips.Value).Distance,
                            },
                            TimeStart = new TimeStartInfo
                            {
                                Id = tknew.IdactiveTripNavigation.IdtimeStart.Value,
                                TimeStart = db.TimeStarts.SingleOrDefault(c => c.IdtimeStart == tknew.IdactiveTripNavigation.IdtimeStart).TimeStart1.Value,
                            },
                            Car = new CarsInfo
                            {
                                Id = tknew.IdactiveTripNavigation.Idcars,
                                NameCar = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).Name,
                                IdType = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).Type.Value,
                                TypeName = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).TypeNavigation.TypeName,
                                PercentType = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).TypeNavigation.PercentType,
                                Model = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).Model,
                                RegistrationDateStart = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).RegistrationDateStart,
                                RegistrationDateEnd = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).RegistrationDateEnd,
                                Photo = db.Cars.SingleOrDefault(c => c.Id.Equals(tknew.IdactiveTripNavigation.Idcars)).Photo
                            },
                            Status = tknew.IdactiveTripNavigation.Status.Value
                        },
                        Seats = tknew.Seats,
                    };
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
           

        }

        public List<TicketInfo> FindAllTicket()
        {
            return db.Tickets.Where(t=>t.StartDate.Value.CompareTo(DateTime.Now)>=0).Select(t => new TicketInfo
            {
                Id = t.Id,
                IdAccCustomer = new AccountInfo
                {
                    Id = t.IdAccCustomer,
                    Username = t.IdAccCustomerNavigation.Username,
                    Password = "",
                    FullName = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(t.IdAccCustomer)).FullName,
                    Email = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(t.IdAccCustomer)).Email,
                    Dob = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(t.IdAccCustomer)).Dob,
                    Photo = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(t.IdAccCustomer)).Photo,
                    NameRole = t.IdAccCustomerNavigation.IdroleNavigation.Name,
                },
                Total = t.Total.Value,
                CreateDate = t.CreateDate,
                Creater = t.Creater,
                StartDate = t.StartDate,
                Age = t.Age,
                IdactiveTrip = new ActiveTripsInfo
                {
                    Id = t.IdactiveTripNavigation.IdtripsTiimeStart,
                    Trip = new TripsInfo
                    {
                        Id = t.IdactiveTripNavigation.Idtrips.Value,
                        NameTrip = db.Trips.SingleOrDefault(c => c.Id == t.IdactiveTripNavigation.Idtrips.Value).NameTrip,
                        FromAddress = t.FromAddress,
                        ToAddress = t.ToAddress,
                        Photo = db.Trips.SingleOrDefault(c => c.Id == t.IdactiveTripNavigation.Idtrips.Value).Photo,
                        Distance = db.Trips.SingleOrDefault(c => c.Id == t.IdactiveTripNavigation.Idtrips.Value).Distance,
                    },
                    TimeStart = new TimeStartInfo
                    {
                        Id = t.IdactiveTripNavigation.IdtimeStart.Value,
                        TimeStart = db.TimeStarts.SingleOrDefault(c => c.IdtimeStart == t.IdactiveTripNavigation.IdtimeStart).TimeStart1.Value,
                    },
                    Car = new CarsInfo
                    {
                        Id = t.IdactiveTripNavigation.Idcars,
                        NameCar = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).Name,
                        IdType = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).Type.Value,
                        TypeName = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).TypeNavigation.TypeName,
                        PercentType = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).TypeNavigation.PercentType,
                        Model = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).Model,
                        RegistrationDateStart = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).RegistrationDateStart,
                        RegistrationDateEnd = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).RegistrationDateEnd,
                        Photo = db.Cars.SingleOrDefault(c => c.Id.Equals(t.IdactiveTripNavigation.Idcars)).Photo
                    },
                    Status = t.IdactiveTripNavigation.Status.Value
                },
                Seats = t.Seats,
            }).ToList();
        }

        public List<SeatsInfo> GetListSeats(int idactivetrip, int dayStart, int monthStart, int yearStart)
        {
            var listSeats = db.Tickets.Where(t => t.IdactiveTrip.Value == idactivetrip && t.StartDate.Value.Day==dayStart && t.StartDate.Value.Month == monthStart && t.StartDate.Value.Year == yearStart).Select(t => new SeatsInfo
            {
                Id = t.Id,
                Seat = t.Seats,
            }).ToList();
            var kq = GetMapSeats().Where(c1 => !listSeats.Any(c2 => c2.Seat.Equals(c1.Seat))).ToList();
            return kq;

        }

        public List<SeatsInfo> GetMapSeats()
        {
            return new List<SeatsInfo>
            {
               new SeatsInfo {
                    Id = 1,
                    Seat = "A1"
                },
                new SeatsInfo {
                    Id = 2,
                    Seat = "B1"
                },
                 new SeatsInfo {
                    Id = 3,
                    Seat = "A2"
                },
                  new SeatsInfo {
                    Id = 4,
                    Seat = "B2"
                },
                   new SeatsInfo {
                    Id = 5,
                    Seat = "A3"
                },
                    new SeatsInfo {
                    Id = 6,
                    Seat = "B3"
                },
                     new SeatsInfo {
                    Id = 7,
                    Seat = "A4"
                },
                      new SeatsInfo {
                    Id = 8,
                    Seat = "B4"
                },
                       new SeatsInfo {
                    Id = 9,
                    Seat = "A5"
                },
                        new SeatsInfo {
                    Id = 10,
                    Seat = "B5"
                },
                         new SeatsInfo {
                    Id = 11,
                    Seat = "A6"
                },
                          new SeatsInfo {
                    Id = 12,
                    Seat = "B6"
                },
                           new SeatsInfo {
                    Id = 13,
                    Seat = "A7"
                },
                            new SeatsInfo {
                    Id = 14,
                    Seat = "B7"
                },
                             new SeatsInfo {
                    Id = 15,
                    Seat = "A8"
                },
                              new SeatsInfo {
                    Id = 16,
                    Seat = "B8"
                },
                               new SeatsInfo {
                    Id = 17,
                    Seat = "A9"
                },
                                new SeatsInfo {
                    Id = 18,
                    Seat = "B9"
                },
                                 new SeatsInfo {
                    Id = 19,
                    Seat = "A10"
                },
                                  new SeatsInfo {
                    Id = 20,
                    Seat = "B10"
                },

            };
        }

        public double GetTotalTam(int idactiveTrip, int age)
        {
            try
            {
              
                var activeTrip = db.ActiveTrips.SingleOrDefault(at => at.IdtripsTiimeStart == idactiveTrip);
                var distance = activeTrip.IdtripsNavigation.Distance.Value;
                var percentTypeCar = activeTrip.IdcarsNavigation.TypeNavigation.PercentType.Value;
                var pricePerKM = db.PricePerKms.Find(1).Price.Value;
                var percentByAge = db.PercentByAges.SingleOrDefault(a => a.AgeStart <= age && a.AgeEnd >= age).PercentDiscount.Value;
                var totaltam = ((pricePerKM * distance) * (percentByAge / 100)) * (percentTypeCar / 100);
                return totaltam;
        }
            catch
            {
                return -1;
            }

        }

        public bool SendMail(string to, string subject, string content, bool isHTML)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enableSsl,
                    Credentials = new NetworkCredential
                    {
                        UserName = email,
                        Password = password
                    }
                };
                var message = new MailMessage(email, to);
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = isHTML;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool Refund(int idTicket)
        {
            var ticketRefund = db.Tickets.SingleOrDefault(t => t.Id == idTicket);
            var daytoStartDate = (ticketRefund.StartDate.Value - DateTime.Now).Days;
            var idrefund = 0;
            if (daytoStartDate >= 2)
            {
                idrefund = 1;

            }else if (daytoStartDate == 1)
            {
                idrefund = 2;
            }
            else if(daytoStartDate ==0)
            {
                idrefund = 3;
            }
            else
            {
                return false;
            }
            var percentRefund = db.Refunds.Find(idrefund).RefundPercent;
            var totalRefund = ticketRefund.Total * percentRefund / 100;
            var refundRequest = new RequestRefund();
            refundRequest.Idrefund = idrefund;
            refundRequest.TotalRefund = totalRefund;
            refundRequest.StatusDone = 0;
            refundRequest.DaysSendRefund = DateTime.Now;
            refundRequest.DaysConfirmRefund = null;
            db.RequestRefunds.Add(refundRequest);
            if (db.SaveChanges() > 0)
            {
                var ticketRequestRefund = new TicketRequestRefund();
                ticketRequestRefund.Idticket = ticketRefund.Id;
                ticketRequestRefund.Idrequest = refundRequest.Idrequest;
                db.TicketRequestRefunds.Add(ticketRequestRefund);
                if (db.SaveChanges() > 0)
                {
                    ticketRefund.StartDate = new DateTime(1900, 01, 01);
                    db.Entry(ticketRefund).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }

               
               
              
            }
            else
            {
                return false;
            }
           
        }
    }
}
