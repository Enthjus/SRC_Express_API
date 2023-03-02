using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class AccountInfo
    {
        public string Id { get; set; }
        [Required] // bat buoc nhap , khong dc rong
        [MaxLength(10)]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime? Dob { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public string NameRole { get; set; }


      
    }
}
