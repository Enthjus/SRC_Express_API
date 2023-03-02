using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class PercentByAgeInfo
    {
        public int Id { get; set; }
        [Required]
        public int? AgeStart { get; set; }
        [Required]
        public int? AgeEnd { get; set; }
        [Required]
        [Range(0,100)]
        public double? PercentDiscount { get; set; }
    }
}