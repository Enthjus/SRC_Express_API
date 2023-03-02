using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRC_Express_API.ModelsInfo
{
    public class CarsInfo
    {
        public string Id { get; set; }
        [Required]
        public string NameCar { get; set; }
        [Required]
        public int IdType { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public double? PercentType { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public DateTime? RegistrationDateStart { get; set; }
        [Required]
        public DateTime? RegistrationDateEnd { get; set; }
        [Required]
        public string Photo { get; set; }
    }
}
