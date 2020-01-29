using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSApp.Models
{
    public class Alteration
    {
        [Key]
        public int Id { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Created;
        public AlterationTypeEnum Type { get; set; }
        [Range(-5,5)]
        [Display(Name = "Left Length")]
        public decimal LeftLength { get; set; }
        [Display(Name = "Right Length")]
        [Range(-5, 5)]
        public decimal RightLength { get; set; }
    }
}
