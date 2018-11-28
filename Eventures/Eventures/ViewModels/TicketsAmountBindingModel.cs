using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class TicketsAmountBindingModel
    {
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Tickets")]
        [Range(0, int.MaxValue, ErrorMessage = "Tickets must be a valid non-negative number.")]
        public int Tickets { get; set; }
    }
}
