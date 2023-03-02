using SRC_Express_API.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class DasboardServiceImpl : DashboardService
    {
        private DatabaseContext db;
        private TripsService tripsService;
        private CarsService carService;
        public DasboardServiceImpl(DatabaseContext _db, TripsService _tripsService, CarsService _carService)
        {
            db = _db;
            tripsService = _tripsService;
            carService = _carService;
        }
        public dynamic Statistical()
        {
            var newOrder = db.Tickets.Where(t => t.CreateDate.Value.Day >= DateTime.Now.Day && t.CreateDate.Value.Month >= DateTime.Now.Month && t.CreateDate.Value.Year == DateTime.Now.Year).Count();

            var totalticket = db.Tickets.Count();
            var totalrequestRefund = db.TicketRequestRefunds.Count();
            double percentRefund = totalrequestRefund / (float)totalticket * 100;

            var customer = db.Accounts.Where(a => a.IdroleNavigation.Name.Equals("Customer")).Count();

            var totalPriceTicket = db.Tickets.Where(t => t.CreateDate.Value.Month == DateTime.Now.Month && t.CreateDate.Value.Year == DateTime.Now.Year).Sum(t => t.Total);
            var totalPriceRefund = db.RequestRefunds.Where(t => t.DaysConfirmRefund.Value.Month == DateTime.Now.Month && t.DaysConfirmRefund.Value.Year == DateTime.Now.Year).Sum(t => t.TotalRefund);
            var monthlyRevenue = totalPriceTicket - totalPriceRefund;

            var activeTrip = db.Tickets.GroupBy(a => a.IdactiveTripNavigation.IdtripsNavigation.NameTrip).Select(g => new
            {
                name = g.Key,
                sumTicket = g.Sum(a=>a.Total),
            }).ToList();


            dynamic kq = new ExpandoObject();
            kq.newOrder = newOrder;
            kq.percentRefund = percentRefund.ToString("F2");
            kq.customer = customer.ToString();
            kq.monthlyRevenue = monthlyRevenue.Value.ToString("N0");
            kq.activeTrip = activeTrip;
            return kq;
        }
    }
}
