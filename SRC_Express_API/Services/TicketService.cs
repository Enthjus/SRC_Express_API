using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public interface TicketService
    {
        public List<TicketInfo> FindAllTicket();
        public TicketInfo CreateTicket(Ticket tk);
        public double GetTotalTam(int idactiveTrip, int age);
        public List<SeatsInfo> GetListSeats(int idactivetrip,int dayStart,int monthStart,int yearStart);
        public Boolean SendMail(string to, string subject, string content, bool isHTML);
        public Boolean Refund(int idTicket);
       


    }
}
