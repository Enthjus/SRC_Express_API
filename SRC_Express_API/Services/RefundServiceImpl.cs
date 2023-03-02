using Microsoft.EntityFrameworkCore;
using SRC_Express_API.Models;
using SRC_Express_API.ModelsInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SRC_Express_API.Services
{
    public class RefundServiceImpl : RefundService
    {
        private DatabaseContext db;
        private static string host = "smtp.gmail.com";
        private static int port = 587;
        private static bool enableSsl = true;
        private static string email = "tranthanhtungdnhhtb@gmail.com";
        private static string password = "anhlaphuc123";
        public RefundServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }

        public List<RefundInfo> FindAllRefund()
        {
            return db.Refunds.Select(p => new RefundInfo
            {
                Id = p.Id,
                DayToStart = p.DayToStart,
                RefundPercent = p.RefundPercent,
            }).ToList();
        }

        public RefundInfo FindRefund(int idRefund)
        {
            var refund = db.Refunds.SingleOrDefault(p => p.Id == idRefund);
            return new RefundInfo
            {
                Id = refund.Id,
                DayToStart = refund.DayToStart,
                RefundPercent = refund.RefundPercent,
            };
        }

        public RefundInfo UpdateRefund(RefundInfo refundInfo)
        {
            var refundupdate = db.Refunds.SingleOrDefault(p => p.Id == refundInfo.Id);
            refundupdate.DayToStart = refundInfo.DayToStart;
            refundupdate.RefundPercent = refundInfo.RefundPercent;
            db.Entry(refundupdate).State = EntityState.Modified;
            db.SaveChanges();
            return new RefundInfo
            {
                Id = refundupdate.Id,
                DayToStart = refundupdate.DayToStart,
                RefundPercent = refundupdate.RefundPercent,
            };
        }

        public List<RequestRefundInfo> FindAllRequestRefund()
        {
            return db.TicketRequestRefunds.Select(t => new RequestRefundInfo
            {
                Idrequest = t.Idrequest.Value,
                IdTicket = t.Idticket.Value,
                PercentRefund = t.IdrequestNavigation.IdrefundNavigation.RefundPercent,
                TotalRefund = t.IdrequestNavigation.TotalRefund,
                StatusDone = t.IdrequestNavigation.StatusDone,
                DaysSendRefund = t.IdrequestNavigation.DaysSendRefund,
                DaysConfirmRefund = t.IdrequestNavigation.DaysConfirmRefund
            }).ToList();
        }

        public bool UpdateStatusRequestRefund(int idRequestRefund)
        {
            var request = db.TicketRequestRefunds.SingleOrDefault(t => t.Idrequest == idRequestRefund).IdrequestNavigation;
            request.StatusDone = 1;
            request.DaysConfirmRefund = DateTime.Now;
            db.Entry(request).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                var user = db.TicketRequestRefunds.SingleOrDefault(t => t.Idrequest == idRequestRefund).IdticketNavigation.IdAccCustomer;
                var email = db.Customers.SingleOrDefault(c => c.Idaccount.Equals(user)).Email;
                var content = "Your Refund Code: " + request.Idrequest + "<br/>";
                content += "Your request has been processed: " + request.DaysConfirmRefund + "<br/>";

                content += "Come to our nearest branch to get your money back. Thanks You!! <br/>";
                return SendMail(email, "Confim Request Refund", content, true);
            }
            else
            {
                return false;
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

                return false;
            }
        }
    }
}
