using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class TripsInfo
    {
        public int Id { get; set; }
        [Required]
        public string NameTrip { get; set; }
        [Required]
        public string FromAddress { get; set; }
        [Required]
        public string ToAddress { get; set; }
        [Required]
        public string Photo { get; set; }
        public double? Distance { get; set; }
    }
}
