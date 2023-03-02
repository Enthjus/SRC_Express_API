using System;
using System.Collections.Generic;

#nullable disable

namespace SRC_Express_API.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string Idaccount { get; set; }
        public string Photo { get; set; }

        public virtual Account IdaccountNavigation { get; set; }
    }
}
