using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class PricePerKmInfo
    {
        public int Id { get; set; }
        [Required]
        [Range(0,1000000)]
        public double? Price { get; set; }
    }
}
