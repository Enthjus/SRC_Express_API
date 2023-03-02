using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class RefundInfo
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 30)]
        public int? DayToStart { get; set; }
        [Required]
        [Range(0, 100)]
        public double? RefundPercent { get; set; }
    }
}
