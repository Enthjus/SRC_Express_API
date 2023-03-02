using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
            Tickets = new HashSet<Ticket>();
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public DateTime? ExpCode { get; set; }
        public int? Idrole { get; set; }
        public int? Status { get; set; }

        public virtual Role IdroleNavigation { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
