using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.ViewModels
{
    public class CreateEventBindingModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(10, ErrorMessage = "Event name must be more than 10 charecters long.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime, ErrorMessage = "Given date must be a valid date.")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End")]
        [DataType(DataType.DateTime, ErrorMessage = "Given date must be a valid date.")]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Price Per Ticket")]
        [Range(0, int.MaxValue, ErrorMessage ="Price must be a valid non-negative number.")]
        public decimal PricePerTicket { get; set; }

        [Required]
        [Display(Name = "Total Tickets")]
        [Range(0, int.MaxValue, ErrorMessage = "Total tickets must be a valid non-negative number.")]
        public int TotalTickets { get; set; }
    }
}
